import { LoginRequest, RegisterRequest } from '../types/types';

export class Repository {
  public static Login(request: LoginRequest) {
    console.log(request.email + ' ' + request.password);
  }

  public static Register(request: RegisterRequest) {
    console.log(
      request.email + ' ' + request.password + ' ' + request.username
    );
  }
}
