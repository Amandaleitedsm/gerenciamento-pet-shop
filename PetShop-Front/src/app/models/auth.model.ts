export interface AuthResponse {
  Token: string;
  Expiration: string;
}

export interface AuthRequest {
  Cpf: string;
  Senha: string;
}
