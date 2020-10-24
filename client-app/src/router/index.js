import Vue from "vue";
import VueRouter from "vue-router";
import store from "@/store";

Vue.use(VueRouter);

const routes = [
  {
    name: "Home",
    path: "/",
    component: () => import("@/views/Home"),
    meta: { requiredAuth: true }
  },
  {
    name: "Login",
    path: "/login",
    component: () => import("@/views/Login"),
    meta: { requiredAuth: false }
  },
  {
    name: "ClientInfo",
    path: "/clientInfo/:id",
    component: () => import("@/views/ClientInfo"),
    meta: { requiredAuth: true }
  },
  {
    name: "ClientList",
    path: "/clientList",
    component: () => import("@/views/ClientList"),
    meta: { requiredAuth: true }
  },
  {
    name: "TicketInfo",
    path: "/ticketInfo/:id",
    component: () => import("@/views/TicketInfo"),
    meta: { requiredAuth: true }
  },
  {
    name: "TicketList",
    path: "/ticketList",
    component: () => import("@/views/TicketList"),
    meta: { requiredAuth: true }
  }
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});

router.beforeEach((to, from, next) => {
  if (to.meta.requiredAuth && !store.getters.getLoginStatus)
    next({ path: "/login" });
  else if (!to.meta.requiredAuth && store.getters.getLoginStatus) next(false);
  else next();
});

export default router;
