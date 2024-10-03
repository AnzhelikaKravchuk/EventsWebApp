import axios, { AxiosResponse } from 'axios';
import { LoginRequest, RegisterRequest, Role } from '../types/types';
import { error } from 'console';

export async function Login(request: LoginRequest): Promise<AxiosResponse> {
  console.log(request);
  return axios.post('https://localhost:7127/login', request, {
    withCredentials: true,
  });
}

export async function Register(
  request: RegisterRequest
): Promise<AxiosResponse> {
  console.log(request);
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
  let response;
  response = await axios
    .get('https://localhost:7127/getRole', {
      withCredentials: true,
    })
    .then((resp) => (response = resp))
    .catch((error) => {
      if (error.status === 401) {
        console.log('Here2');
        TryRefreshToken()
          .then((res) => (response = res))
          .catch((err) => {
            if (err.status === 401) {
              Logout();
            }
          });
        axios.get('https://localhost:7127/getRole', {
          withCredentials: true,
        });
      }
    });
  console.log(response?.data as Role);
  return response?.data as Role;
}

export async function TryRefreshToken(): Promise<AxiosResponse> {
  console.log('Refresh');
  const response = await axios.post(
    'https://localhost:7127/refresh',
    {},
    {
      withCredentials: true,
    }
  );

  return response;
}

export async function Logout() {
  const response = await axios.get('https://localhost:7127/logout', {
    withCredentials: true,
  });

  return response;
}
