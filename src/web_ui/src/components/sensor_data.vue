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
      <div class="filter-wrapper">
        <div class="sort-div">
          <p>Sort by:</p>
          <select name="cars" id="cars">
            <option value="volvo">ID</option>
            <option value="volvo">Type</option>
            <option value="saab">Date</option>
            <option value="opel">Occupancy</option>
            <option value="audi">Weight</option>
          </select>
        </div>
        <div class="filter-div">
          <p>Filter:</p>
          <select name="cars" id="cars">
            <option value="volvo">ID</option>
            <option value="volvo">Type</option>
            <option value="saab">Date</option>
            <option value="opel">Occupancy</option>
            <option value="audi">Weight</option>
          </select>
        </div>
      </div>
      <div class="scroll">
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
        <div v-if="isloading">
          <p>LOADING...</p>
        </div>
      </div>
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
import {onMounted, ref, watch} from 'vue'
import {get_sensor_data_from_api} from "@/controller/sensor_data";
import {useInterval} from "@vueuse/core";

const {counter, pause, resume} = useInterval(1000, {controls: true})
watch(counter, async () => {
  pause()
  rows.value = await get_sensor_data_from_api()
  resume()
})


function scrollToElement(id: string) {
  const element = document.getElementById(id);
  console.log(element)
  element!.scrollIntoView({
    behavior: "smooth"
  });
}

let rows = ref()

onMounted(async () => {
  rows.value = await get_sensor_data_from_api()
})

</script>


<style scoped>

.overlay {
  background-color: #242528;
  /*position: fixed;*/
  /*z-index: 1;*/
  width: 100%;
  /*height: 100%;*/
  /*top: 0;*/
  /*left: 0;*/
  /*overflow-y: auto;*/
}

.main-title {
  margin-top: 10px;
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

.filter-wrapper {
  margin-top: 40px;
  width: 750px;
  display: flex;
  justify-content: center;
  margin-left: auto;
  margin-right: auto;
}

.sort-div {
  margin-left: auto;
  margin-right: auto;
  float: left;
  width: 100%;
}

.filter-div {
  margin-left: auto;
  margin-right: auto;
  float: right;
  width: 100%;
}

.data-section {
  height: 600px;
}

.scroll {
  margin-top: 40px;
  overflow-y: scroll;
  width: 750px;
  display: flex;
  justify-content: center;
  height: 400px;
  margin-left: auto;
  margin-right: auto;
}

.table-root {
  margin-left: auto;
  margin-right: auto;
  border-collapse: collapse;
  background-color: #2E2F32;
  width: 750px;
  text-align: left;
  height: 400px;
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
  height: 2em;
}

td {
  height: 2em;
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
