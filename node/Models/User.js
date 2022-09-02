class User {
  constructor(id, email, displayName, phoneNumber, photoURL, status) {
    this.id = id;
    this.email = email;
    this.displayName = displayName;
    this.phoneNumber = phoneNumber;
    this.photoURL = photoURL;
    this.status = status;
  }
}

module.exports = User;
