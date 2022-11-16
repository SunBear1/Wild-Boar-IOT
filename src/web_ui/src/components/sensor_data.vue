<template>
  <div class="overlay">
    <nav id="navbar" class="navbar">
      <p class="nav-title"> {{ NAV_TITLE }}</p>
      <div class="nav-items">
        <a @click="scrollToElement('charts')" class="nav-button">{{ NAV_CHARTS_TITLE }}</a>
        <a @click="scrollToElement('data')" class="nav-button">{{ NAV_TABLE_TITLE }}</a>
        <a @click="scrollToElement('dashboard')" class="nav-button">{{ NAV_DASHBOARD_TITLE }}</a>
      </div>
    </nav>

    <div id="dashboard" class="dashboard-section">
      <p class="main-title">Dashboard</p>
    </div>

    <div id="data" class="data-section">
      <p class="main-title">{{ TABLE_TITLE }}</p>
      <table class="table-root">
        <tr>
          <th>ID</th>
          <th>Type</th>
          <th>Date</th>
          <th>Occupancy</th>
          <th>Weight</th>
        </tr>
        <tr v-for="row in rows">
          <td>{{ row.id }}</td>
          <td>{{ row.type }}</td>
          <td>{{ row.date }}</td>
          <td>{{ row.occupied }}</td>
          <td>{{ row.weight }}</td>
        </tr>
      </table>
    </div>

    <div id="charts" class="charts-section">
      <p class="main-title">Charts</p>
    </div>

    <div class="footer">
      <p class="nav-title-footer" @click="scrollToElement('navbar')"> {{ NAV_TITLE }}</p>
      <div class="tmp">
        <p class="footer-text">Serwisy internetowe .NET - projekt</p>
        <p class="footer-text">2022 &#169; Copyright Łukasz Niedźwiadek, Jakub Sachajko, Łukasz Zaleski</p>
      </div>
    </div>
  </div>

</template>

<script setup lang="ts">
import {NAV_CHARTS_TITLE, NAV_DASHBOARD_TITLE, NAV_TABLE_TITLE, NAV_TITLE, TABLE_TITLE} from "@/constants/texts";
import {onMounted, ref} from 'vue'
import {get_sensor_data_from_api} from "@/controller/sensor_data";

function scrollToElement(id: string) {
  const element = document.getElementById(id);
  console.log(element)
  element!.scrollIntoView({
    behavior: "smooth"
  });
}

let rows = ref()
let tmp

onMounted(async () => {
  tmp = await get_sensor_data_from_api()
  rows.value = tmp
  console.log("rows")
  console.log(rows.value)
})

</script>


<style scoped>

.overlay {
  background-color: #242528;
  position: fixed;
  z-index: 1;
  width: 100%;
  height: 100%;
  top: 0;
  left: 0;
  overflow-y: scroll;
}

.main-title {
  margin-top: 10px;
  align-content: center;
  text-align: center;
  font-size: 1.5rem;
  color: white;
  font-family: Tahoma, Helvetica, Arial, sans-serif;
}

.footer {
  left: 0;
  bottom: 0;
  width: 100%;
  background-color: #00C7FD;
  color: white;
  font-size: 10px;
  text-align: center;
  align-content: center;
  height: 30px;
  padding-top: 7px;
}

.nav-title-footer {
  float: left;
  text-align: center;
  font-size: 1rem;
  color: #2E2F32;
  font-family: Tahoma, Helvetica, Arial, sans-serif;
  margin-left: 80px;
  cursor: pointer;
  z-index: 3;
}

.footer-text {
  font-family: Tahoma, Helvetica, Arial, sans-serif;
  color: #2E2F32;
  font-size: 8px;
  text-align: center;
}

/*Data section styles*/

.data-section {
  height: 800px;
}


.table-root {
  margin-top: 35px;
  margin-left: auto;
  margin-right: auto;
  border-collapse: collapse;
  border-spacing: 5px;
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

/*Charts section*/

.charts-section {
  height: 500px;
}

/*Dashboard section*/

.dashboard-section {
  height: 300px;
}

/*Nav Bar styles*/
.navbar {
  display: flex;
  align-items: center;
  height: 55px;
  background-color: #2E2F32;
  margin-bottom: 40px;
}

.nav-title {
  text-align: left;
  font-size: 1.5rem;
  color: white;
  font-family: Tahoma, Helvetica, Arial, sans-serif;
  margin-left: 10px;
  text-decoration: none;
  border-bottom: 4px solid transparent;
}

.nav-items {
  float: left;
  margin-left: 50px;
  margin-right: auto;
}

.nav-button {
  float: right;
  display: block;
  color: white;
  text-align: center;
  padding: 14px 16px;
  background-color: #2E2F32;
  font-size: 1rem;
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
