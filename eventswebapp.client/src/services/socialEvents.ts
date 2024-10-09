import axios from 'axios';
import { SocialEventModel, SocialEventsResponse } from '../types/types';

const host = import.meta.env.VITE_API_HOST;

export async function GetSocialEvents(
  filters: FormData,
  pageIndex: number,
  pageSize: number
): Promise<SocialEventsResponse> {
  const response = await axios.post(
    `${host}/SocialEvents?pageIndex=${pageIndex}&pageSize=${pageSize}`,
    filters,
    {
      withCredentials: true,
    }
  );
  return {
    ...response.data,
    items: response.data.items.$values,
  } as SocialEventsResponse;
}

export async function GetSocialEventByIdWithToken(
  id: string
): Promise<SocialEventModel> {
  const response = await axios.get(
    `${host}/SocialEvents/getSocialEventByIdWithToken?id=${id}`,
    {
      withCredentials: true,
    }
  );
  return {
    ...response.data,
    listOfAttendees: response.data.listOfAttendees.$values,
  } as SocialEventModel;
}

export async function GetSocialEventById(
  id: string
): Promise<SocialEventModel> {
  const response = await axios.get(
    `${host}/SocialEvents/getSocialEventById?id=${id}`,
    {
      withCredentials: true,
    }
  );
  return {
    ...response.data,
    listOfAttendees: response.data.listOfAttendees.$values,
  } as SocialEventModel;
}

export async function EditEvent(socialEvent: FormData): Promise<boolean> {
  const response = await axios
    .put(`${host}/SocialEvents/updateEvent`, socialEvent, {
      withCredentials: true,
    })
    .then(() => true)
    .catch(() => false);
  return response;
}

export async function CreateEvent(socialEvent: FormData): Promise<string> {
  const response = await axios
    .post(`${host}/SocialEvents/createEvent`, socialEvent, {
      withCredentials: true,
    })
    .then((res) => res.data)
    .catch(() => null);
  return response;
}

export async function DeleteEvent(id: string): Promise<boolean> {
  const response = await axios
    .delete(`${host}/SocialEvents/deleteEvent?id=${id}`, {
      withCredentials: true,
    })
    .then(() => true)
    .catch(() => false);
  return response;
}

export async function UploadImage(
  formData: FormData,
  id: string
): Promise<boolean> {
  const response = await axios
    .put(`${host}/SocialEvents/upload?id=${id}`, formData, {
      withCredentials: true,
    })
    .then(() => true)
    .catch(() => false);

  return response;
}
