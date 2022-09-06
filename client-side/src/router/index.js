import { getAuth, onAuthStateChanged } from "firebase/auth";
import Vue from "vue";
import VueRouter from "vue-router";

const originalPush = VueRouter.prototype.push;
VueRouter.prototype.push = function push(location, onResolve, onReject) {
  if (onResolve || onReject)
    return originalPush.call(this, location, onResolve, onReject);
  return originalPush.call(this, location).catch((err) => err);
};

Vue.use(VueRouter);

//Routes
import base from "./routes/index";
import users from "./routes/users";
import admin from "./routes/admin";
import movies from "./routes/movies";
import cinemas from "./routes/cinemas";
import events from "./routes/events";

const router = new VueRouter({
  scrollBehavior() {
    return { x: 0, y: 0 };
  },
  mode: "history",
  base: process.env.BASE_URL,
  routes: [
    ...base,
    ...users,
    ...admin,
    ...movies,
    ...cinemas,
    ...events,
    {
      path: "/login",
      name: "auth-login",
      component: () => import("@/views/Authorization/Login/Login.vue"),
      meta: {
        isPublic: true,
        layout: "public",
        resource: "Auth",
        redirectIfLoggedIn: true,
      },
    },
  ],
});

const getCurrentUser = () => {
  return new Promise((resolve, reject) => {
    const removeListener = onAuthStateChanged(
      getAuth(),
      (user) => {
        removeListener();
        resolve(user);
      },
      reject
    );
  });
};

router.beforeEach(async (to, from, next) => {
  if (to.matched.some((record) => record.meta.requiresAuth)) {
    if (getCurrentUser) {
      next();
    } else {
      alert("You dont have access!");
      next("/");
    }
  } else {
    next();
  }
});

export default router;
