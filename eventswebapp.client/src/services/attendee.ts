import axios from 'axios';
import { AttendeeModel, CreateAttendeeRequest } from '../types/types';

const host = import.meta.env.VITE_API_HOST;

export async function GetAttendeesByUser(): Promise<Array<AttendeeModel>> {
  const response = await axios.get(`${host}/Attendee`, {
    withCredentials: true,
  });
  return response.data.$values as Array<AttendeeModel>;
}

export async function AddAttendeeToEvent(
  data: CreateAttendeeRequest,
  eventId: string
): Promise<boolean> {
  const response = await axios
    .post(`${host}/Attendee/addAttendee?eventId=${eventId}`, data, {
      withCredentials: true,
    })
    .then(() => true)
    .catch(() => false);
  return response;
}

export async function DeleteAttendee(attendeeId: string): Promise<boolean> {
  const response = await axios
    .delete(`${host}/Attendee?id=${attendeeId}`, {
      withCredentials: true,
    })
    .then(() => true)
    .catch(() => false);
  return response;
}
