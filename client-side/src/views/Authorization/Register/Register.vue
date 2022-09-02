<template>
  <body class="d-flex justify-content-center form-wrap">
    <form class="register">
      <p class="login-register">
        Already have an Account?
        <router-link class="router-link" :to="{ name: 'Login' }">
          <h2>Login</h2>
        </router-link>
      </p>
      <h2>Create your CinemaVerse Account</h2>
      <button @click.prevent="signInWithGoogle">Google Sign Up</button>
      <div class="inputs">
        <div class="input">
          <input type="text" placeholder="Display Name" v-model="displayName" />
        </div>
        <div class="input">
          <input type="text" placeholder="Photo url" v-model="photoURL" />
        </div>
        <div class="input">
          <input type="text" placeholder="Email" v-model="email" />
        </div>
        <div class="input">
          <input type="password" placeholder="Password" v-model="password" />
        </div>
        <div class="error" v-if="error">
          {{ this.errorMsg }}
        </div>
      </div>
      <button @click.prevent="submitForm">Sign Up</button>
    </form>
    <div class="background"></div>
  </body>
</template>

<script>
import {
  getAuth,
  createUserWithEmailAndPassword,
  GoogleAuthProvider,
  signInWithPopup,
} from "firebase/auth";

export default {
  data() {
    return {
      email: null,
      displayName: null,
      password: null,
      photoURL: null,
      phoneNumber: null,
      roles: [0],
      status: 1,
      error: null,
      errorMsg: null,
    };
  },

  methods: {
    signInWithGoogle() {
      const provider = new GoogleAuthProvider();
      signInWithPopup(getAuth(), provider).then((result) => {
        console.log(result.user);
        this.$router.push("/");
      });
    },
    submitForm() {
      if (this.isFormValid()) {
        this.error = false;
        this.errorMsg = null;
        createUserWithEmailAndPassword(getAuth(), this.email, this.password)
          .then((result) => {
            console.log("result", result);
            // TODO: fix to save results to firestore database
            // const database = db.collection("users").doc(result.user.uid);

            // dataBase.set({
            //   id: result.user.uid,
            //   email: this.email,
            //   displayName: this.displayName,
            //   phoneNumber: this.phoneNumber,
            //   photoURL: this.photoURL,
            //   roles: this.roles,
            //   status: this.status,
            // });
            console.log("User registered!");
            this.$router.push("/home");
          })
          .catch((err) => {
            console.log(err.code);
            alert(err.message);
          });
      } else {
        this.error = true;
        this.errorMsg = "Please fill out all the fields!";
      }
    },
    isFormValid() {
      if (
        this.firstName !== null &&
        this.lastName !== null &&
        this.email !== null &&
        this.password !== null
      ) {
        return true;
      } else {
        return false;
      }
    },
  },
};
</script>

<style></style>
