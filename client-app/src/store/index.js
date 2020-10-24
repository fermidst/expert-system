import Vue from "vue";
import Vuex from "vuex";
import auth from "./modules/auth";
import client from "./modules/client";
import ticket from "./modules/ticket";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {},
  mutations: {},
  actions: {},
  modules: { auth, client, ticket }
});
