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
  User = 'user',
  Admin = 'admin',
  Guest = 'guest',
}
export interface SocialEventModel {
  title: string;
  description: string;
  place: string;
  date: Date;
  category: string;
  maxAttendee: number;
  attendees: Array<AttendeeModel>;
  image: Uint8Array;
}

export interface AttendeeModel {
  name: string;
  surname: string;
  email: string;
  dateOfAdmission: Date;
  dateOfBirth: Date;
}
export interface MetaData {
  currentPage: number;
  totalPages: number;
  pageSize: number;
  totalCount: number;
}

export class PaginateResponse<T> {
  items: T;
  metaData: MetaData;

  constructor(items: T, metadta: MetaData) {
    this.items = items;
    this.metaData = metadta;
  }
}
export interface PageParams {
  orderBy: string;
  searchTerm?: string;
  pageNumber: number;
  pageSize: number;
  columnFilters: string;
}
