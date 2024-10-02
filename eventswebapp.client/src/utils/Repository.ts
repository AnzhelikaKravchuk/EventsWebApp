import { LoginRequest, RegisterRequest, Role } from '../types/types';
const items = [
  {
    id: '1',
    eventName: 'Lol',
    description: 'Description',
    place: 'Gomel',
    date: new Date(),
    category: 'LEcture',
    maxAttendee: 1,
    attendees: [],
    image: '',
  },
  {
    id: '2',
    eventName: 'Extravaganza',
    description: 'Description',
    place: 'Polotsk',
    date: new Date(),
    category: 'Other',
    maxAttendee: 1,
    attendees: [],
    image: '',
  },
  {
    id: '3',
    eventName: 'Climate Change',
    description: 'Description',
    place: 'Minsk',
    date: new Date(),
    category: 'Conference',
    maxAttendee: 1,
    attendees: [],
    image: '',
  },
];

export class Repository {
  public static async Login(request: LoginRequest): Promise<boolean> {
    console.log(request.email + ' ' + request.password);

    return true;
  }

  public static async GetRole() {
    console.log('Here');
    return Role.Admin;
  }

  public static async Register(request: RegisterRequest): Promise<boolean> {
    console.log(
      request.email + ' ' + request.password + ' ' + request.username
    );
    return true;
  }

  public static async GetSocialEvents(pageIndex: number, pageSize: number) {
    const endOffset = (pageIndex - 1) * pageSize + pageSize;
    const currentItems = items.slice((pageIndex - 1) * pageSize, endOffset);
    const pageCount = Math.ceil(items.length / pageSize);

    return { currentItems, pageCount };
  }
  public static async CreateEvent(request) {
    console.log(request);
    items.push(request);
    return true;
  }
}
