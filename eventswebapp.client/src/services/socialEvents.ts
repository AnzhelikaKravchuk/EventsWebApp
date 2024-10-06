import axios from 'axios';
import {
  AppliedFilters,
  CreateSocialEventRequest,
  EditSocialEventRequest,
  SocialEventModel,
  SocialEventsResponse,
} from '../types/types';

export async function GetSocialEvents(
  filters: FormData,
  pageIndex: number,
  pageSize: number
): Promise<SocialEventsResponse> {
  const response = await axios.post(
    `https://localhost:7127/SocialEvents?pageIndex=${pageIndex}&pageSize=${pageSize}`,
    filters,
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

export async function GetSocialEventById(
  id: string
): Promise<SocialEventModel> {
  console.log(id);
  const response = await axios.get(
    `https://localhost:7127/SocialEvents/getSocialEventById?id=${id}`,
    {
      withCredentials: true,
    }
  );
  // .catch((error: AxiosError) => {
  //   if (error.status === 401) {
  //     return TryRefreshToken(GetSocialEvents);
  //   }
  // });
  return response.data as SocialEventModel;
}

export async function EditEvent(
  socialEvent: EditSocialEventRequest
): Promise<boolean> {
  console.log(socialEvent);

  const response = await axios
    .put(`https://localhost:7127/SocialEvents/updateEvent`, socialEvent, {
      withCredentials: true,
    })
    .then(() => true)
    .catch(() => false);

  // .catch((error: AxiosError) => {
  //   if (error.status === 401) {
  //     return TryRefreshToken(GetSocialEvents);
  //   }
  // });
  return response;
}

export async function CreateEvent(
  socialEvent: FormData
): Promise<SocialEventModel> {
  console.log(socialEvent);
  const response = await axios
    .post(`https://localhost:7127/SocialEvents/createEvent`, socialEvent, {
      withCredentials: true,
    })
    .then((res) => res.data)
    .catch(() => null);

  // .catch((error: AxiosError) => {
  //   if (error.status === 401) {
  //     return TryRefreshToken(GetSocialEvents);
  //   }
  // });
  return response;
}

export async function DeleteEvent(id: string): Promise<boolean> {
  const response = await axios
    .delete(`https://localhost:7127/SocialEvents/deleteEvent?id=${id}`, {
      withCredentials: true,
    })
    .then(() => true)
    .catch(() => false);

  // .catch((error: AxiosError) => {
  //   if (error.status === 401) {
  //     return TryRefreshToken(GetSocialEvents);
  //   }
  // });
  return response;
}

export async function UploadImage(
  formData: FormData,
  id: string
): Promise<boolean> {
  const response = await axios
    .put(`https://localhost:7127/SocialEvents/upload?id=${id}`, formData, {
      withCredentials: true,
    })
    .then(() => true)
    .catch(() => false);

  return response;
}
