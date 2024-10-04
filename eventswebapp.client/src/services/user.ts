import axios, { AxiosResponse } from 'axios';
import { LoginRequest, RegisterRequest, Role } from '../types/types';

export async function Login(request: LoginRequest): Promise<AxiosResponse> {
  return axios.post('https://localhost:7127/login', request, {
    withCredentials: true,
  });
}

export async function Register(
  request: RegisterRequest
): Promise<AxiosResponse> {
  const response = await axios.post(
    'https://localhost:7127/register',
    request,
    {
      withCredentials: true,
    }
  );

  return response;
}

export async function GetRole(): Promise<Role> {
  const response: Role = await axios
    .get('https://localhost:7127/getRole', {
      withCredentials: true,
    })
    .then((response) => response?.data)
    .catch(async (error) => {
      if (error.status === 401) {
        console.log('Here2');
        return TryRefreshToken(GetRole);
      }
    });
  return response ?? Role.Guest;
}

export async function TryRefreshToken<Data>(
  callback: () => Promise<Data>
): Promise<Data | null> {
  const response: Data | null = await axios
    .post(
      'https://localhost:7127/refresh',
      {},
      {
        withCredentials: true,
      }
    )
    .then(() => {
      return callback();
    })
    .catch(() => {
      Logout();
      return null;
    });

  return response;
}

export async function Logout() {
  const response = await axios.get('https://localhost:7127/logout', {
    withCredentials: true,
  });

  return response;
}
