import axios, { AxiosResponse } from 'axios';
import { PaginateResponse } from '../types/types';

axios.defaults.baseURL = 'https://localhost:/';

const responseBody = (response: AxiosResponse) => response.data;

axios.interceptors.request.use((config) => {
  return config;
});

axios.interceptors.response.use(
  (response) => {
    const pagination = response.headers['pagination'];
    if (pagination) {
      response.data = new PaginateResponse(
        response.data,
        JSON.parse(pagination)
      );
      return response;
    }
    return response;
  },
  (error) => {
    console.log(error);
  }
);

const requests = {
  get: (url: string, params?: URLSearchParams, cancelToken?: any) =>
    axios.get(url, { params, cancelToken }).then(responseBody),
};

export default requests;
