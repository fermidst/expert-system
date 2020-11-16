<template>
  <MainLayout>
    <template v-slot:icon>
      <v-icon @click="debugList = !debugList">mdi-diamond-stone</v-icon>
    </template>
    <div v-if="debugList">
      <div :key="jewelry.id" v-for="jewelry in jewelries">
        {{ jewelry }}
      </div>
    </div>

    <v-data-table
      :headers="headers"
      :items="jewelries"
      sort-by="id"
      class="elevation-4 ma-4 pa-4"
      disable-pagination
      hide-default-footer
      :loading="jewelryListInfo.isLoading"
    >
      <template v-slot:top>
        <v-toolbar flat>
          <v-toolbar-title>Ювелирные изделия</v-toolbar-title>
          <v-divider class="mx-4" inset vertical></v-divider>
          <v-spacer></v-spacer>
          <v-dialog v-model="dialog" max-width="800px">
            <template v-slot:activator="{ on, attrs }">
              <v-btn color="primary" dark v-bind="attrs" v-on="on">
                Новое изделие
              </v-btn>
            </template>
            <v-card>
              <v-card-title>
                <span class="headline">{{ formTitle }}</span>
              </v-card-title>

              <v-card-text>
                <v-container>
                  <v-row justify="space-around">
                    <v-col cols="12" sm="6" md="4">
                      <v-text-field
                        v-model="editedItem.name"
                        label="Название"
                      ></v-text-field>
                    </v-col>
                    <v-col cols="12" sm="6" md="3">
                      <v-text-field label="Цена" v-model="editedItem.price" />
                    </v-col>
                  </v-row>
                  <v-row justify="space-around">
                    <v-col cols="12" sm="6" md="3">
                      <v-text-field
                        label="Ссылка на фотографию"
                        v-model="editedItem.photoUrl"
                      />
                    </v-col>
                    <v-col cols="12" md="4">
                      <v-menu
                        v-model="startDateMenu"
                        :close-on-content-click="false"
                        max-width="290"
                      >
                        <template v-slot:activator="{ on, attrs }">
                          <v-text-field
                            :value="moment(editedItem.receivedDate)"
                            clearable
                            label="Дата получения на склад"
                            readonly
                            v-bind="attrs"
                            v-on="on"
                            @click:clear="editedItem.receivedDate = null"
                          ></v-text-field>
                        </template>
                        <v-date-picker
                          v-model="editedItem.receivedDate"
                          @change="startDateMenu = false"
                        ></v-date-picker>
                      </v-menu>
                    </v-col>
                  </v-row>
                </v-container>
              </v-card-text>

              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="blue darken-1" text @click="close">
                  Отмена
                </v-btn>
                <v-btn color="blue darken-1" text @click="save">
                  ОК
                </v-btn>
              </v-card-actions>
            </v-card>
          </v-dialog>
          <v-dialog v-model="dialogDelete" max-width="700px">
            <v-card>
              <v-card-title class="headline justify-center"
                >Вы уверены, что хотите удалить это изделие?</v-card-title
              >
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="blue darken-1" text @click="closeDelete"
                  >Cancel</v-btn
                >
                <v-btn color="blue darken-1" text @click="deleteItemConfirm"
                  >OK</v-btn
                >
                <v-spacer></v-spacer>
              </v-card-actions>
            </v-card>
          </v-dialog>
        </v-toolbar>
      </template>
      <template v-slot:item.receivedDate="{ item }">
        {{ moment(item.receivedDate) }}
      </template>
      <template #item.photoUrl="{item}">
        <v-expansion-panels accordion>
          <v-expansion-panel>
            <v-expansion-panel-header>
              Изображение ювелирного изделия</v-expansion-panel-header
            >
            <v-expansion-panel-content>
              <v-container>
                <v-row>
                  <v-col>
                    <v-img :src="item.photoUrl" height="500" width="500" />
                  </v-col>
                </v-row>
              </v-container>
            </v-expansion-panel-content>
          </v-expansion-panel>
        </v-expansion-panels>
      </template>
      <template v-slot:item.actions="{ item }">
        <v-icon small class="mr-2" @click="editItem(item)">
          mdi-pencil
        </v-icon>
        <v-icon small @click="deleteItem(item)">
          mdi-delete
        </v-icon>
      </template>
    </v-data-table>
  </MainLayout>
</template>

<script>
import MainLayout from "@/components/layout/MainLayout";
import moment from "moment";
export default {
  name: "JewelryList",
  components: { MainLayout },
  data: () => ({
    startDateMenu: false,
    debugList: false,
    dialog: false,
    dialogDelete: false,
    headers: [
      {
        text: "Id",
        align: "start",
        value: "id"
      },
      { text: "Название", value: "name" },
      { text: "Цена", value: "price" },
      { text: "Дата получения на склад", value: "receivedDate" },
      { text: "Изображение", value: "photoUrl" },
      { text: "Действия", value: "actions", sortable: false }
    ],
    editedItem: {
      id: 0,
      name: "",
      price: 0,
      receivedDate: "",
      photoUrl: ""
    },
    defaultItem: {}
  }),
  created() {
    this.$store.dispatch("fetchJewelryListInfo");
  },
  computed: {
    jewelryListInfo: {
      get() {
        return this.$store.state.jewelry.jewelryListInfo;
      }
    },
    jewelries: {
      get() {
        return this.$store.state.jewelry.jewelryListInfo.value;
      }
    },
    formTitle: {
      get() {
        return this.editedIndex === -1
          ? "Новое изделие"
          : "Редактировать изделие";
      }
    }
  },

  methods: {
    moment(date) {
      return moment(date).format("LL");
    },

    editItem(item) {
      this.editedItem = Object.assign({}, item);
      this.dialog = true;
    },

    deleteItem(item) {
      this.editedItem = Object.assign({}, item);
      this.dialogDelete = true;
    },

    deleteItemConfirm() {
      this.$store.dispatch("deleteJewelryInfo", { id: this.editedItem.id });
      this.closeDelete();
    },

    close() {
      this.dialog = false;
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
      });
    },

    closeDelete() {
      this.dialogDelete = false;
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
      });
    },

    save() {
      if (this.editedItem.id > 0) {
        this.$store.dispatch("putJewelryInfo", {
          id: this.editedItem.id,
          data: this.editedItem
        });
      } else {
        this.$store.dispatch("postJewelryInfo", {
          data: this.editedItem
        });
      }
      this.close();
    }
  }
};
</script>

<style></style>
