import Vue from "vue";
import Vuetify from "vuetify/lib";
import ru from "vuetify/es5/locale/ru";
import { preset } from "vue-cli-plugin-vuetify-preset-crane/preset";

Vue.use(Vuetify);

export default new Vuetify({
  preset,
  lang: {
    locales: { ru },
    current: "ru"
  }
});
