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
      <p class="dashboard-small-title">Last received message: <span>{{ }}</span></p>
      <br>
      <p class="dashboard-small-title">Avarage values per 100 messages for each type:</p>
      <br>
      <p class="dashboard-small-title">Chest machine weight: <span>PLACEHOLDER</span></p>
      <p class="dashboard-small-title">Biceps machine weight: <span>PLACEHOLDER</span></p>
      <p class="dashboard-small-title">Treadmill weight: <span>PLACEHOLDER</span></p>
      <br>
      <p class="dashboard-small-title">Chest machine occupancy: <span>PLACEHOLDER</span></p>
      <p class="dashboard-small-title">Biceps machine occupancy: <span>PLACEHOLDER</span></p>
      <p class="dashboard-small-title">Treadmill occupancy: <span>PLACEHOLDER</span></p>
    </div>

    <div id="data" class="data-section">
      <p class="main-title">{{ TABLE_TITLE }}</p>
      <div class="filter-wrapper">
        <div class="sort-div">
          <p class="filter-title">Sort by</p>
          <select name="sorting-form" id="sort" v-model="sort_input">
            <option value="id">ID</option>
            <option value="type">Type</option>
            <option value="date">Date</option>
            <option value="occupied">Occupancy</option>
            <option value="weight">Weight</option>
          </select>
          <br>
          <br>
          <p class="filter-title">Sorting order</p>
          <select name="sorting-order-form" v-model="sorting_order_input">
            <option value="asc">Ascending</option>
            <option value="desc">Descending</option>
          </select>
        </div>
        <div class="filter-div">
          <p class="filter-title">Type</p>
          <select name="filtering-form" id="filter" v-model="type_input">
            <option value="all">All</option>
            <option value="CHEST_MACHINE">Chest machine</option>
            <option value="BICEPS_MACHINE">Biceps machine</option>
            <option value="TREADMILL">Treadmill</option>
          </select>
          <br>
          <br>
          <p class="filter-title">Data format</p>
          <select name="format-filter-form" v-model="format_input">
            <option value="application/json">JSON</option>
            <option value="text/csv">CSV</option>
          </select>
        </div>
        <div class="filter-div">
          <p class="filter-title">From</p>
          <input type="date" name="date-start-filter-form"
                 min="01-01-2021" max="12-31-2021" v-model="date_start_input">
          <br>
          <br>
          <p class="filter-title">To the</p>
          <input type="date" name="date-end-filter-form"
                 min="01-02-2021" max="01-01-2022" v-model="date_end_input">
        </div>
        <div class="filter-div">
          <p class="filter-title">Occupancy</p>
          <select name="occupancy-filter-form" v-model="occupancy_input">
            <option value="all">All</option>
            <option value="true">True</option>
            <option value="false">False</option>
          </select>
          <br>
          <br>
          <p class="filter-title">Weight</p>
          <input name="weight-filter-form" type="number" min="0" step="1" v-model="weight_input">
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
      </div>
    </div>

    <div id="charts" class="charts-section">
      <p class="main-title">Charts</p>
      <charts chartData="charts_obj"/>
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
import {collect_parameters, parse_parameters} from "@/controller/parameters";
import Charts from "@/components/charts.vue";

let rows = ref()
let sort_input = ref("id")
let type_input = ref("all")
let date_start_input = ref(undefined)
let date_end_input = ref(undefined)
let occupancy_input = ref("all")
let weight_input = ref("")
let format_input = ref("application/json")
let sorting_order_input = ref("asc")


const {counter, pause, resume} = useInterval(1000, {controls: true})
watch(counter, async () => {
  pause()
  let url_parameters: string[] = collect_parameters(sort_input.value, type_input.value, occupancy_input.value, weight_input.value, sorting_order_input.value, date_start_input.value, date_end_input.value)
  rows.value = await get_sensor_data_from_api(format_input.value, parse_parameters(url_parameters))
  resume()
})

function scrollToElement(id: string) {
  const element = document.getElementById(id);
  console.log(element)
  element!.scrollIntoView({
    behavior: "smooth"
  });
}

let chart_obj = {
  labels: ['January', 'February', 'March'],
  datasets: [
    {
      label: 'Data One',
      backgroundColor: '#f87979',
      data: [40, 20, 12]
    }
  ]
}

onMounted(async () => {
  rows.value = await get_sensor_data_from_api(format_input.value, parse_parameters([]))
})

</script>

<style scoped>

.overlay {
  background-color: #242528;
  width: 100%;
}

.main-title {
  margin-top: 10px;
  margin-bottom: 20px;
  text-align: center;
  font-size: 1.5rem;
  color: white;
  font-family: Tahoma, Helvetica, Arial, sans-serif;
}

.dashboard-small-title {
  color: white;
  font-size: 1rem;
  text-align: center;
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

.filter-title {
  color: white;
  font-size: 14px;
}

option {
  background-color: #2E2F32;
  color: white;
}

select {
  background-color: #00C7FD;
  font-weight: bold;
  color: #242528;
  border-radius: 5px;
}

input {
  background-color: #00C7FD;
  font-weight: bold;
  color: #242528;
  border-radius: 5px;
  padding-left: 5px;
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
  max-height: 400px;
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
  max-height: 400px;
}

th {
  background-color: #00C7FD;
  font-weight: bold;
  color: #242528;
  font-family: Tahoma, Helvetica, Arial, sans-serif;
  padding: 8px;
  font-size: 14px;
  height: 2em;
}

td {
  height: 2em;
  font-family: Tahoma, Helvetica, Arial, sans-serif;
  color: white;
  padding: 8px;
  font-size: 14px;
}

thead th {
  width: 25%;
}

/*Charts section*/

.charts-section {
  height: 800px;
}

/*Dashboard section*/

.dashboard-section {
  height: 300px;
  align-items: center;
  justify-content: center;
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
