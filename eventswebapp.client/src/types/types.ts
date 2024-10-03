export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  email: string;
  password: string;
  username: string;
}

export enum Role {
  User = 'User',
  Admin = 'Admin',
  Guest = 'Guest',
}
export interface SocialEventModel {
  id: string;
  eventName: string;
  description: string;
  place: string;
  date: Date;
  category: string;
  maxAttendee: number;
  attendees: Array<AttendeeModel>;
  image: string;
}

export interface AttendeeModel {
  id: string;
  name: string;
  surname: string;
  email: string;
  dateOfBirth: Date;
  dateOfAdmission: Date;
}
