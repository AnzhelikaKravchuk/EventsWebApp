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

export interface AppliedFilters {
  name: string;
  date: Date;
  place: string;
  category: string;
}
export interface SocialEventModel {
  id: string;
  eventName: string;
  description: string;
  place: string;
  date: string;
  category: string;
  maxAttendee: number;
  listOfAttendees: Array<AttendeeModel>;
  image: string;
}

export interface EditSocialEventRequest {
  id: string;
  eventName: string;
  description: string;
  place: string;
  date: Date;
  category: string;
  maxAttendee: number;
}

export interface CreateSocialEventRequest {
  eventName: string;
  description: string;
  place: string;
  date: Date;
  category: string;
  maxAttendee: number;
}

export interface SocialEventsResponse {
  items: Array<SocialEventModel>;
  pageIndex: number;
  totalPages: number;
  hasPrevious: boolean;
  hasNext: boolean;
}

export interface AttendeeModel {
  id: string;
  name: string;
  surname: string;
  email: string;
  dateOfBirth: Date;
  dateOfRegistration: Date;
  socialEventName: string;
}

export interface CreateAttendeeRequest {
  name: string;
  surname: string;
  email: string;
  dateOfBirth: Date;
}
