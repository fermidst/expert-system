import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import vuetify from "./plugins/vuetify";

import "vue-datetime/dist/vue-datetime.css";
import Axios from "axios";

Vue.config.productionTip = false;

Axios.defaults.baseURL = process.env.VUE_BASE_URL || "https://localhost:5000";

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount("#app");
