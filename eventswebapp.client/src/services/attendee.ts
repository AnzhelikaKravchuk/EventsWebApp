import axios from 'axios';
import { AttendeeModel, CreateAttendeeRequest } from '../types/types';

export async function GetAttendeesByUser(): Promise<Array<AttendeeModel>> {
  const response = await axios.get(`https://localhost:7127/Attendee`, {
    withCredentials: true,
  });

  console.log(response);
  // .catch((error: AxiosError) => {
  //   if (error.status === 401) {
  //     return TryRefreshToken(GetSocialEvents);
  //   }
  // });
  return response.data.$values as Array<AttendeeModel>;
}

export async function AddAttendeeToEvent(
  data: CreateAttendeeRequest,
  eventId: string
): Promise<boolean> {
  const response = await axios
    .post(`https://localhost:7127/Attendee?eventId=${eventId}`, data, {
      withCredentials: true,
    })
    .then(() => true)
    .catch(() => false);

  console.log(response);
  // .catch((error: AxiosError) => {
  //   if (error.status === 401) {
  //     return TryRefreshToken(GetSocialEvents);
  //   }
  // });
  return response;
}

export async function DeleteAttendee(attendeeId: string): Promise<boolean> {
  const response = await axios
    .delete(`https://localhost:7127/Attendee?id=${attendeeId}`, {
      withCredentials: true,
    })
    .then(() => true)
    .catch(() => false);

  console.log(response);
  // .catch((error: AxiosError) => {
  //   if (error.status === 401) {
  //     return TryRefreshToken(GetSocialEvents);
  //   }
  // });
  return response;
}
