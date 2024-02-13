export class RegisterUser {
    constructor(
      public firstName?: string,
      public lastName?: string,
      public phoneNumber?: string,
      public dob?: Date,
      public email?: string,
      public password?: string
    ) {}
  }
  