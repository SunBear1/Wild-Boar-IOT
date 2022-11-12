<template>
  <div class="overlay">
    <nav class="navbar">
      <a href="/" class="nav-title"> {{ NAV_TITLE }}</a>
      <div class="nav-items">
        <a href="#" class="nav-button">{{ NAV_DASHBOARD_TITLE }}</a>
        <a href="#" class="nav-button">{{ NAV_TABLE_TITLE }}</a>
      </div>

    </nav>
    <br>
    <div class="main-section">
      <p class="main-title">{{ TABLE_TITLE }}</p>
      <table class="table-root">
        <tr>
          <th>ID</th>
          <th>Date</th>
          <th>Occupancy</th>
          <th>Weight</th>
        </tr>
        <tr v-for="row in rows">
          <td>{{ row.id }}</td>
          <td>2022-12-24</td>
          <td>{{ row.occupancy }}</td>
          <td>{{ row.weight }}</td>
        </tr>
      </table>
    </div>
  </div>
</template>

<script setup lang="ts">
import {NAV_DASHBOARD_TITLE, NAV_TABLE_TITLE, NAV_TITLE, TABLE_TITLE} from "@/constants/texts";
import {onMounted, ref} from 'vue'
import {get_sensor_data_from_api} from "@/controller/sensor_data";

let rows = ref()
let tmp

onMounted(async () => {
  tmp = await get_sensor_data_from_api()
  rows.value = tmp
  console.log(rows.value)
})

</script>


<style scoped>

.overlay {
  background-color: #242528;
  position: fixed;
  z-index: 1000;
  width: 100%;
  height: 100%;
  top: 0;
  left: 0;
}

.main-title {
  margin-top: 10px;
  text-align: center;
  font-size: 1.5rem;
  color: white;
  font-family: Tahoma, Helvetica, Arial, sans-serif;
}

.table-root {
  margin-top: 35px;
  margin-left: auto;
  margin-right: auto;
  border-collapse: collapse;
  border-spacing: 5px;
  /*border: 1px solid white;*/
  background-color: #2E2F32;
  width: 30%;
  text-align: left;
}

th, td {
  font-family: Tahoma, Helvetica, Arial, sans-serif;
  color: white;
  padding: 8px;
  font-size: 14px;
}

th {
  background-color: #00C7FD;
  font-weight: bold;
  color: #242528;
}

thead th {
  width: 25%;
}

.navbar {
  display: flex;
  align-items: center;
  height: 55px;
  background-color: #2E2F32;
}

.nav-title {
  text-align: left;
  font-size: 1.5rem;
  color: white;
  font-family: Tahoma, Helvetica, Arial, sans-serif;
  margin-left: 10px;
  text-decoration: none;
}

.nav-items {
  float: left;
  margin-left: auto;
  margin-right: 35px;
}

.nav-button {
  float: left;
  display: block;
  color: white;
  text-align: center;
  padding: 14px 16px;
  background-color: #2E2F32;
  font-size: 13px;
  text-decoration: none;
  cursor: pointer;
  border-bottom: 4px solid transparent;
}

.nav-button:hover {
  border-bottom: 4px solid #00C7FD;
}

.nav-button:active {
  border-bottom: 4px solid #00C7FD;
}
</style>
