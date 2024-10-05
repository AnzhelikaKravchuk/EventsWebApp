import axios, { AxiosError } from 'axios';
import { TryRefreshToken } from './user';
import { SocialEventModel, SocialEventsResponse } from '../types/types';

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
  // .catch((error: AxiosError) => {
  //   if (error.status === 401) {
  //     return TryRefreshToken(GetSocialEvents);
  //   }
  // });
  return response.data as SocialEventsResponse;
}

export async function EditEvent(
  socialEvent: SocialEventModel
): Promise<boolean> {
  console.log(socialEvent);

  const response = await axios.put(
    `https://localhost:7127/SocialEvents/updateEvent`,
    socialEvent,
    {
      withCredentials: true,
    }
  );

  // .catch((error: AxiosError) => {
  //   if (error.status === 401) {
  //     return TryRefreshToken(GetSocialEvents);
  //   }
  // });
  return true;
}
