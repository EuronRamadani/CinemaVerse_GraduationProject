import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import { BootstrapVue, IconsPlugin } from "bootstrap-vue";
import { initializeApp } from "firebase/app";

import "@/assets/css/style.css";
import "bootstrap/dist/css/bootstrap.css";
import "bootstrap-vue/dist/bootstrap-vue.css";
import "material-design-icons-iconfont/dist/material-design-icons.css";

// Global Components
import "./global-components";
//  Global Helpers
import helpers from "./helpers/helpers";

import vuetify from "./plugins/vuetify";

Vue.use(BootstrapVue);
Vue.use(IconsPlugin);

Vue.mixin(helpers);

Vue.config.productionTip = false;

const firebaseConfig = {
  apiKey: "AIzaSyAnYLc54geuQzqysf2161E0ZmLH7-GKo44",
  authDomain: "cinemaverse-13e76.firebaseapp.com",
  projectId: "cinemaverse-13e76",
  storageBucket: "cinemaverse-13e76.appspot.com",
  messagingSenderId: "502334150744",
  appId: "1:502334150744:web:17548aaaf23dabaf81922d",
};

initializeApp(firebaseConfig);

new Vue({
  router,
  store,
  vuetify,
  render: (h) => h(App),
}).$mount("#app");
