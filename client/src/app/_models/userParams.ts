import { User } from './user';

export class UserParams {
  gender = 'all';
  minAge = 18;
  maxAge = 99;
  pageNumber = 1;
  pageSize = 24;
  orderBy = 'lastActive';

  constructor(user: User) {}
}
