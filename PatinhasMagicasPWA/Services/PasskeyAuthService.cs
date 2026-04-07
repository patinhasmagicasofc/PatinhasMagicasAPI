using Microsoft.JSInterop;
using PatinhasMagicasPWA.DTOs;
using System.Net.Http.Json;
using System.Text.Json;

namespace PatinhasMagicasPWA.Services
{
    public class PasskeyAuthService
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _jsRuntime;
        private readonly TokenStorageService _tokenStorageService;
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public PasskeyAuthService(HttpClient http, IJSRuntime jsRuntime, TokenStorageService tokenStorageService)
        {
            _http = http;
            _jsRuntime = jsRuntime;
            _tokenStorageService = tokenStorageService;
        }

        public async Task<bool> IsSupportedAsync()
        {
            return await _jsRuntime.InvokeAsync<bool>("passkeys.isSupported");
        }

        public async Task<PasskeyOperationResultDTO> RegisterCurrentDeviceAsync()
        {
            try
            {
                var beginResponse = await _http.PostAsync("api/passkeys/register/options", null);
                if (!beginResponse.IsSuccessStatusCode)
                {
                    return new PasskeyOperationResultDTO
                    {
                        Success = false,
                        Message = await ExtractErrorMessageAsync(beginResponse)
                    };
                }

                var options = await beginResponse.Content.ReadFromJsonAsync<PasskeyOptionsResponseDTO>(JsonOptions);
                if (options is null)
                {
                    return new PasskeyOperationResultDTO
                    {
                        Success = false,
                        Message = "Nao foi possivel iniciar o cadastro da biometria."
                    };
                }

                var credential = await _jsRuntime.InvokeAsync<JsonElement>("passkeys.createCredential", options.PublicKey);

                var completeResponse = await _http.PostAsJsonAsync("api/passkeys/register/complete", new PasskeyCompleteRequestDTO
                {
                    FlowId = options.FlowId,
                    Credential = credential
                });

                if (!completeResponse.IsSuccessStatusCode)
                {
                    return new PasskeyOperationResultDTO
                    {
                        Success = false,
                        Message = await ExtractErrorMessageAsync(completeResponse)
                    };
                }

                return await completeResponse.Content.ReadFromJsonAsync<PasskeyOperationResultDTO>(JsonOptions)
                    ?? new PasskeyOperationResultDTO
                    {
                        Success = true,
                        Message = "Biometria cadastrada com sucesso."
                    };
            }
            catch (JSException ex)
            {
                return new PasskeyOperationResultDTO
                {
                    Success = false,
                    Message = NormalizeJsMessage(ex.Message)
                };
            }
            catch
            {
                return new PasskeyOperationResultDTO
                {
                    Success = false,
                    Message = "Nao foi possivel cadastrar a biometria agora."
                };
            }
        }

        public async Task<LoginResultDTO> LoginWithPasskeyAsync()
        {
            try
            {
                var beginResponse = await _http.PostAsync("api/passkeys/login/options", null);
                if (!beginResponse.IsSuccessStatusCode)
                {
                    return new LoginResultDTO
                    {
                        Sucesso = false,
                        Mensagem = await ExtractErrorMessageAsync(beginResponse)
                    };
                }

                var options = await beginResponse.Content.ReadFromJsonAsync<PasskeyOptionsResponseDTO>(JsonOptions);
                if (options is null)
                {
                    return new LoginResultDTO
                    {
                        Sucesso = false,
                        Mensagem = "Nao foi possivel iniciar o login com biometria."
                    };
                }

                var credential = await _jsRuntime.InvokeAsync<JsonElement>("passkeys.getCredential", options.PublicKey);

                var completeResponse = await _http.PostAsJsonAsync("api/passkeys/login/complete", new PasskeyCompleteRequestDTO
                {
                    FlowId = options.FlowId,
                    Credential = credential
                });

                var content = await completeResponse.Content.ReadAsStringAsync();

                if (!completeResponse.IsSuccessStatusCode)
                {
                    return new LoginResultDTO
                    {
                        Sucesso = false,
                        Mensagem = AuthService.ExtractErrorMessage(content)
                    };
                }

                var loginResponse = JsonSerializer.Deserialize<LoginResponseDTO>(content, JsonOptions);
                var token = loginResponse?.Data?.Token;

                if (string.IsNullOrWhiteSpace(token))
                {
                    return new LoginResultDTO
                    {
                        Sucesso = false,
                        Mensagem = loginResponse?.Message ?? "A API nao retornou um token valido para o login com biometria."
                    };
                }

                await _tokenStorageService.SetToken(token);

                return new LoginResultDTO
                {
                    Sucesso = true
                };
            }
            catch (JSException ex)
            {
                return new LoginResultDTO
                {
                    Sucesso = false,
                    Mensagem = NormalizeJsMessage(ex.Message)
                };
            }
            catch
            {
                return new LoginResultDTO
                {
                    Sucesso = false,
                    Mensagem = "Erro ao conectar com a API."
                };
            }
        }

        public async Task<List<PasskeyCredentialDTO>> GetMyCredentialsAsync()
        {
            var response = await _http.GetAsync("api/passkeys/mine");
            if (!response.IsSuccessStatusCode)
            {
                return new List<PasskeyCredentialDTO>();
            }

            return await response.Content.ReadFromJsonAsync<List<PasskeyCredentialDTO>>(JsonOptions) ?? new List<PasskeyCredentialDTO>();
        }

        public async Task<PasskeyOperationResultDTO> RemoveCredentialAsync(int credentialId)
        {
            var response = await _http.DeleteAsync($"api/passkeys/{credentialId}");
            if (!response.IsSuccessStatusCode)
            {
                return new PasskeyOperationResultDTO
                {
                    Success = false,
                    Message = await ExtractErrorMessageAsync(response)
                };
            }

            return new PasskeyOperationResultDTO
            {
                Success = true,
                Message = "Biometria removida com sucesso."
            };
        }

        private static string NormalizeJsMessage(string rawMessage)
        {
            if (string.IsNullOrWhiteSpace(rawMessage))
            {
                return "A operacao com biometria foi cancelada.";
            }

            if (rawMessage.Contains("NotAllowedError", StringComparison.OrdinalIgnoreCase))
            {
                return "A biometria foi cancelada ou negada no dispositivo.";
            }

            return rawMessage;
        }

        private static async Task<string> ExtractErrorMessageAsync(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            return AuthService.ExtractErrorMessage(content);
        }
    }
}
