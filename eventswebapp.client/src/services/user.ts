import axios, { AxiosResponse } from 'axios';
import { LoginRequest, RegisterRequest, Role } from '../types/types';

const host = import.meta.env.VITE_API_HOST;

export async function Login(request: LoginRequest): Promise<AxiosResponse> {
  return axios.post(`${host}/login`, request, {
    withCredentials: true,
  });
}

export async function Register(
  request: RegisterRequest
): Promise<AxiosResponse> {
  const response = await axios.post(`${host}/register`, request, {
    withCredentials: true,
  });

  return response;
}

export async function GetRole(): Promise<Role> {
  const response: Role = await axios
    .get(`${host}/getRole`, {
      withCredentials: true,
    })
    .then((response) => response?.data);

  return response ?? Role.Guest;
}

export async function TryRefreshToken(): Promise<AxiosResponse> {
  const response = await axios.post(
    `${host}/refresh`,
    {},
    {
      withCredentials: true,
    }
  );

  return response;
}

export async function Logout() {
  const response = await axios.get(`${host}/logout`, {
    withCredentials: true,
  });

  return response;
}

export async function RetryFetch<Data>(
  callback: () => Promise<Data>
): Promise<Data> {
  const response = await TryRefreshToken();
  if (response.status !== 200) {
    throw Error();
  }

  return callback();
}
