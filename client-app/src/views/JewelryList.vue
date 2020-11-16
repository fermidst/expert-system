<template>
  <MainLayout>
    <template v-slot:icon>
      <v-icon @click="debugList = !debugList">mdi-airplane</v-icon>
    </template>
    <!--    <template v-slot:title>Список путевок</template>-->
    <v-snackbar v-model="snackbar" color="red darken-2"
      >Неправильная дата вылета или прибытия</v-snackbar
    >
    <div v-if="debugList">
      <div :key="ticket.id" v-for="ticket in tickets">
        {{ ticket }}
      </div>
    </div>

    <div v-if="!ticketListInfo.isLoading">
      <v-data-table
        :headers="headers"
        :items="tickets"
        sort-by="id"
        class="elevation-4 ma-4 pa-4"
        disable-pagination
        hide-default-footer
      >
        <template v-slot:top>
          <v-toolbar flat>
            <v-toolbar-title>Путевки</v-toolbar-title>
            <v-divider class="mx-4" inset vertical></v-divider>
            <v-spacer></v-spacer>
            <v-dialog v-model="dialog" max-width="800px">
              <template v-slot:activator="{ on, attrs }">
                <v-btn color="primary" dark v-bind="attrs" v-on="on">
                  Новая путевка
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
                          v-model="editedItem.address"
                          label="Адрес"
                        ></v-text-field>
                      </v-col>
                      <v-col cols="12" sm="6" md="4">
                        <v-select
                          :items="[1, 2, 3, 4, 5]"
                          label="Класс отеля"
                          v-model="editedItem.hotelClass"
                        ></v-select>
                      </v-col>
                      <v-col cols="12" sm="6" md="3">
                        <v-checkbox
                          label="Все включено"
                          v-model="editedItem.isAllInclusive"
                        />
                      </v-col>
                    </v-row>
                    <v-row justify="space-around">
                      <v-col cols="12" md="4">
                        <v-menu
                          v-model="startDateMenu"
                          :close-on-content-click="false"
                          max-width="290"
                        >
                          <template v-slot:activator="{ on, attrs }">
                            <v-text-field
                              :value="moment(editedItem.startDate)"
                              clearable
                              label="Дата вылета"
                              readonly
                              v-bind="attrs"
                              v-on="on"
                              @click:clear="editedItem.startDate = null"
                            ></v-text-field>
                          </template>
                          <v-date-picker
                            v-model="editedItem.startDate"
                            @change="startDateMenu = false"
                          ></v-date-picker>
                        </v-menu>
                      </v-col>
                      <v-col cols="12" md="4">
                        <v-menu
                          v-model="endDateMenu"
                          :close-on-content-click="false"
                          max-width="290"
                        >
                          <template v-slot:activator="{ on, attrs }">
                            <v-text-field
                              :value="moment(editedItem.endDate)"
                              clearable
                              label="Дата прибытия"
                              readonly
                              v-bind="attrs"
                              v-on="on"
                              @click:clear="editedItem.endDate = null"
                            ></v-text-field>
                          </template>
                          <v-date-picker
                            v-model="editedItem.endDate"
                            @change="endDateMenu = false"
                          ></v-date-picker>
                        </v-menu>
                      </v-col>
                    </v-row>
                    <v-row justify="space-around">
                      <v-col cols="12" md="4">
                        <v-menu
                          ref="startTimeMenu"
                          v-model="startTimeMenu"
                          :close-on-content-click="false"
                          :nudge-right="40"
                          :return-value.sync="editedItem.startTime"
                          transition="scale-transition"
                          offset-y
                          max-width="290px"
                          min-width="290px"
                        >
                          <template v-slot:activator="{ on, attrs }">
                            <v-text-field
                              v-model="editedItem.startTime"
                              label="Время вылета"
                              prepend-icon="mdi-clock-time-four-outline"
                              readonly
                              v-bind="attrs"
                              v-on="on"
                            ></v-text-field>
                          </template>
                          <v-time-picker
                            v-if="startTimeMenu"
                            v-model="editedItem.startTime"
                            full-width
                            @click:minute="
                              $refs.startTimeMenu.save(editedItem.startTime)
                            "
                          ></v-time-picker>
                        </v-menu>
                      </v-col>
                      <v-col cols="12" md="4">
                        <v-menu
                          ref="endTimeMenu"
                          v-model="endTimeMenu"
                          :close-on-content-click="false"
                          :nudge-right="40"
                          :return-value.sync="editedItem.endTime"
                          transition="scale-transition"
                          offset-y
                          max-width="290px"
                          min-width="290px"
                        >
                          <template v-slot:activator="{ on, attrs }">
                            <v-text-field
                              v-model="editedItem.endTime"
                              label="Время прибытия"
                              prepend-icon="mdi-clock-time-four-outline"
                              readonly
                              v-bind="attrs"
                              v-on="on"
                            ></v-text-field>
                          </template>
                          <v-time-picker
                            v-if="endTimeMenu"
                            v-model="editedItem.endTime"
                            full-width
                            @click:minute="
                              $refs.endTimeMenu.save(editedItem.endTime)
                            "
                          ></v-time-picker>
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
                  >Вы уверены, что хотите удалить эту путевку?</v-card-title
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
        <template v-slot:item.startDate="{ item }">
          {{ moment(item.startDate) }} {{ item.startTime }}
        </template>
        <template v-slot:item.endDate="{ item }">
          {{ moment(item.endDate) }} {{ item.endTime }}
        </template>
        <template v-slot:item.actions="{ item }">
          <v-icon small class="mr-2" @click="editItem(item)">
            mdi-pencil
          </v-icon>
          <v-icon small @click="deleteItem(item)">
            mdi-delete
          </v-icon>
        </template>
        <template v-slot:item.isAllInclusive="{ item }">
          <v-simple-checkbox v-model="item.isAllInclusive" disabled />
        </template>
      </v-data-table>
    </div>
    <div align="center" v-else>
      <v-progress-circular indeterminate size="72" />
    </div>
  </MainLayout>
</template>

<script>
import MainLayout from "@/components/layout/MainLayout";
import moment from "moment";
export default {
  name: "TicketList",
  components: { MainLayout },
  data: () => ({
    startDateMenu: false,
    endDateMenu: false,
    startTimeMenu: false,
    endTimeMenu: false,
    debugList: false,
    snackbar: false,
    dialog: false,
    dialogDelete: false,
    headers: [
      {
        text: "Id",
        align: "start",
        value: "id"
      },
      { text: "Адрес", value: "address" },
      { text: "Класс отеля", value: "hotelClass" },
      { text: "Дата вылета", value: "startDate" },
      { text: "Дата прибытия", value: "endDate" },
      { text: "Все включено", value: "isAllInclusive" },
      { text: "Действия", value: "actions", sortable: false }
    ],
    editedIndex: -1,
    editedItem: {
      id: 0,
      address: "",
      hotelClass: 0,
      startDate: "",
      endDate: "",
      startTime: "",
      endTime: "",
      isAllInclusive: false
    },
    defaultItem: {
      id: 0,
      address: "",
      hotelClass: 0,
      startDate: "",
      endDate: "",
      startTime: "",
      endTime: "",
      isAllInclusive: false
    }
  }),
  created() {
    this.$store.dispatch("fetchTicketListInfo");
  },
  computed: {
    ticketListInfo: {
      get() {
        return this.$store.state.ticket.ticketListInfo;
      }
    },
    tickets: {
      get() {
        return this.$store.state.ticket.ticketListInfo.value;
      }
    },
    formTitle: {
      get() {
        return this.editedIndex === -1
          ? "Новая путевка"
          : "Редактировать путевку";
      }
    }
  },

  methods: {
    moment(date) {
      return moment(date).format("LL");
    },

    editItem(item) {
      this.editedIndex = this.tickets.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.dialog = true;
    },

    deleteItem(item) {
      this.editedIndex = this.tickets.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.dialogDelete = true;
    },

    deleteItemConfirm() {
      this.$store.dispatch("deleteTicketInfo", { id: this.editedItem.id });
      this.closeDelete();
    },

    close() {
      this.dialog = false;
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      });
    },

    closeDelete() {
      this.dialogDelete = false;
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      });
    },

    save() {
      if (
        moment(this.editedItem.startDate) < new Date() ||
        moment(this.editedItem.endDate) < new Date()
      ) {
        this.snackbar = true;
      }
      if (this.editedIndex > -1) {
        this.$store.dispatch("putTicketInfo", {
          id: this.editedItem.id,
          data: this.editedItem
        });
      } else {
        this.$store.dispatch("postTicketInfo", {
          data: this.editedItem
        });
      }
      this.close();
    }
  }
};
</script>

<style></style>
