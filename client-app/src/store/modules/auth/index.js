import router from "@/router";

const state = {
  loginStatus: false
};

const getters = {
  getLoginStatus: state => state.loginStatus
};

const mutations = {
  setLoginStatus(state, loginStatus) {
    state.loginStatus = loginStatus;
  }
};

const actions = {
  login(context, { name, password }) {
    if (name === "admin" && password === "admin") {
      context.commit("setLoginStatus", true);
      router.push("/");
    } else alert("Неправильный логин или пароль");
  },
  logout(context) {
    context.commit("setLoginStatus", false);
    router.push("/login");
  }
};

export default { state, getters, mutations, actions };
