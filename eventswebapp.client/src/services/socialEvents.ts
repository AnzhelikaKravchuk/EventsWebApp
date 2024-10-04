import axios, { AxiosError } from 'axios';
import { TryRefreshToken } from './user';
import { SocialEventsResponse } from '../types/types';

export async function GetSocialEvents(
  pageIndex: number,
  pageSize: number
): Promise<SocialEventsResponse> {
  const response = await axios.get(
    `https://localhost:7127/SocialEvents?pageIndex=${pageIndex}&pageSize=${pageSize}`,
    {
      withCredentials: true,
    }
  );
  console.log(response);

  console.log(response.data as SocialEventsResponse);
  // .catch((error: AxiosError) => {
  //   if (error.status === 401) {
  //     return TryRefreshToken(GetSocialEvents);
  //   }
  // });
  return response.data as SocialEventsResponse;
}
