export class User {
  constructor(
    public firstName: string,
    public lastName: string,
    public phoneNumber: string,
    public dob: Date,
    public email: string,
    public password: string
  ) {}
}
