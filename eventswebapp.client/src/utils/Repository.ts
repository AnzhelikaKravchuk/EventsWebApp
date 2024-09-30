import { LoginRequest, RegisterRequest, Role } from '../types/types';

export class Repository {
  public static async Login(request: LoginRequest): Promise<boolean> {
    console.log(request.email + ' ' + request.password);

    return true;
  }

  public static async GetRole() {
    console.log('Here');
    return Role.User;
  }

  public static async Register(request: RegisterRequest): Promise<boolean> {
    console.log(
      request.email + ' ' + request.password + ' ' + request.username
    );
    return true;
  }
}
