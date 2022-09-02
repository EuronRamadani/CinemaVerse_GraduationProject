<template>
  <body class="d-flex justify-content-center form-wrap">
    <v-form ref="form" v-model="valid" lazy-validation>
      <div class="mb-2">
        <h2 class="mb-5">Create your CinemaVerse Account</h2>
      </div>

      <div class="google-button mb-4">
        <v-btn block color="primary" x-large @click.prevent="signInWithGoogle">
          <v-avatar size="40">
            <img :src="google" />
          </v-avatar>
          <span class="ml-5"> Sign Up With Google </span>
        </v-btn>
        <v-btn fab icon> </v-btn>
      </div>

      <v-row wrap no-gutters>
        <v-col cols="12" class="text-center">
          <v-divider vertical />
        </v-col>
        <v-col cols="12" class="text-center"> <h3>Or</h3> </v-col>
        <v-col cols="12" class="text-center mb-4">
          <v-divider vertical />
        </v-col>
      </v-row>

      <v-text-field
        v-model="displayName"
        :rules="nameRules"
        label="Display name"
        prepend-inner-icon="person"
        required
        outlined
      />

      <v-text-field
        v-model="photoURL"
        label="Photo Url"
        prepend-inner-icon="photo_camera"
        required
        outlined
      />

      <v-text-field
        type="text"
        placeholder="Email"
        v-model="email"
        :rules="emailRules"
        prepend-inner-icon="email"
        label="E-mail"
        required
        outlined
      />

      <v-text-field
        v-model="password"
        :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'"
        :rules="[rules.required]"
        :type="show1 ? 'text' : 'password'"
        name="input-10-1"
        prepend-inner-icon="key"
        label="Password"
        outlined
        @click:append="show1 = !show1"
      />
      <div v-if="errorMsg">
        <h6 class="error--text">
          {{ errorMsg }}
        </h6>
      </div>
      <div class="my-2">
        <v-btn
          block
          large
          :disabled="!valid"
          color="success"
          @click.prevent="submitForm"
        >
          Submit
        </v-btn>
      </div>

      <hr />
      Already have an account?
      <v-btn outlined color="primary" :to="{ name: 'Login' }"> Login </v-btn>
    </v-form>
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
      google: require("@/assets/google-logo2.png"),
      valid: false,
      show1: false,
      emailRules: [
        (v) => !!v || "E-mail is required",
        (v) => /.+@.+/.test(v) || "E-mail must be valid",
      ],

      rules: {
        required: (value) => !!value || "Required.",
        min: (v) => v.length >= 8 || "Min 8 characters",
      },
      nameRules: [
        (v) => !!v || "Name is required",
        (v) => v.length <= 10 || "Name must be less than 10 characters",
      ],
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
    validate() {
      this.$refs.form.validate();
    },
    submitForm() {
      this.validate;
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
            this.$router.push("/home");
          })
          .catch((err) => {
            this.errorMsg = err.code;
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
