export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  email: string;
  password: string;
  username: string;
}

export enum Role {
  User = 'user',
  Admin = 'admin',
  Guest = 'guest',
}
