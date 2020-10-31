<template>
  <MainLayout>
    <template v-slot:icon>
      <v-icon>mdi-shield-account</v-icon>
    </template>
    <!--    <template v-slot:title>Вход</template>-->

    <v-row class="align-center justify-center d-flex">
      <v-col cols="12" md="6" lg="4" xl="3">
        <v-card elevation="12" class="pa-4">
          <v-form
            @submit.prevent="() => false"
            ref="form"
            v-model="valid"
            lazy-validation
          >
            <v-text-field
              v-model="name"
              :counter="10"
              :rules="nameRules"
              label="Имя"
              required
            ></v-text-field>

            <v-text-field
              v-model="password"
              :rules="passwordRules"
              type="password"
              label="Пароль"
              required
            ></v-text-field>

            <v-btn
              type="submit"
              :disabled="!valid"
              color="success"
              @click="login"
            >
              Войти
            </v-btn>
          </v-form>
        </v-card>
      </v-col>
    </v-row>
  </MainLayout>
</template>

<script>
import MainLayout from "@/components/layout/MainLayout";
export default {
  name: "Login",
  components: { MainLayout },
  data() {
    return {
      valid: true,
      name: "",
      nameRules: [v => !!v || "Требуется имя"],
      password: "",
      passwordRules: [v => !!v || "Требуется пароль"]
    };
  },
  methods: {
    login() {
      this.$refs.form.validate();
      if (this.valid)
        this.$store.dispatch("login", {
          name: this.name,
          password: this.password
        });
    }
  }
};
</script>

<style scoped></style>
