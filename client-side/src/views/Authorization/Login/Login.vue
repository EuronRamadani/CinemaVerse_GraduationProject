<template>
  <body class="d-flex justify-content-center form-wrap">
    <form class="login">
      <h2>Login to your CinemaVerse Account</h2>
      <button @click.prevent="signInWithGoogle">Google Sign In</button>
      <div class="inputs">
        <div class="input">
          <input type="text" placeholder="Email" v-model="email" />
        </div>
        <div class="input">
          <input type="password" placeholder="Password" v-model="password" />
        </div>
        <div class="error" v-if="errorMsg">
          {{ errorMsg }}
        </div>
      </div>
      <button @click.prevent="submitForm">Sign In</button>
    </form>
    <div class="background"></div>
  </body>
</template>

<script>
import {
  getAuth,
  signInWithEmailAndPassword,
  GoogleAuthProvider,
  signInWithPopup,
} from "firebase/auth";
export default {
  data() {
    return {
      email: null,
      password: null,
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
        this.errorMsg = null;
        signInWithEmailAndPassword(getAuth(), this.email, this.password)
          .then(() => {
            console.log("Successfully signed in!");
            this.$router.push("/home");
          })
          .catch((err) => {
            this.errorMsg = err.code;
          });
      } else {
        this.errorMsg = "Please fill out all the fields!";
      }
    },
    isFormValid() {
      if (this.email !== null && this.password !== null) {
        return true;
      } else {
        return false;
      }
    },
  },
};
</script>
