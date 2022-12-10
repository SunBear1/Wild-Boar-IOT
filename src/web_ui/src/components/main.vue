<template>
  <div class="overlay">
    <nav id="navbar" class="navbar">
      <p class="nav-title"> {{ NAV_TITLE }}</p>
      <div class="nav-items">
        <a class="nav-button" @click="scrollToElement('charts')">{{ NAV_CHARTS_TITLE }}</a>
        <a class="nav-button" @click="scrollToElement('data')">{{ NAV_TABLE_TITLE }}</a>
        <a class="nav-button" @click="scrollToElement('dashboard')">{{ NAV_DASHBOARD_TITLE }}</a>
      </div>
    </nav>

    <div v-if="dashboard_data" id="dashboard" class="dashboard-section">
      <p class="main-title">Dashboard</p>
      <p class="dashboard-small-title">Last received message:</p>
      <div class="dashboard-wrapper">
        <div class="dashboard-container">
          <p class="dashboard-container-text">ID: <span>{{ dashboard_data.last_msg.id }}</span></p>
          <p class="dashboard-container-text">Type: <span>{{ dashboard_data.last_msg.type }}</span></p>
          <p class="dashboard-container-text">Date: <span>{{ dashboard_data.last_msg.date }}</span></p>
          <p class="dashboard-container-text">Occupancy: <span>{{ dashboard_data.last_msg.occupied }}</span></p>
          <p class="dashboard-container-text">Weight: <span>{{ dashboard_data.last_msg.weight }}</span></p>
        </div>
      </div>
      <br>
      <p class="dashboard-small-title">Avarage values per 100 messages for each type:</p>
      <br>
      <p class="dashboard-small-title">Chest machine weight: <span>{{ dashboard_data.chest_avg_weight }}</span></p>
      <p class="dashboard-small-title">Biceps machine weight: <span>{{ dashboard_data.biceps_avg_weight }}</span></p>
      <p class="dashboard-small-title">Treadmill weight: <span>{{ dashboard_data.treadmill_avg_weight }}</span></p>
      <br>
      <p class="dashboard-small-title">Chest machine occupancy: <span>{{ dashboard_data.chest_avg_occupancy }}</span>
      </p>
      <p class="dashboard-small-title">Biceps machine occupancy: <span>{{ dashboard_data.biceps_avg_occupancy }}</span>
      </p>
      <p class="dashboard-small-title">Treadmill occupancy: <span>{{ dashboard_data.treadmill_avg_occupancy }}</span>
      </p>
    </div>

    <div id="data" class="data-section">
      <p class="main-title">{{ TABLE_TITLE }}</p>
      <div class="filter-wrapper">
        <div class="sort-div">
          <p class="filter-title">Sort by</p>
          <select id="sort" v-model="sort_input" name="sorting-form">
            <option value="id">ID</option>
            <option value="type">Type</option>
            <option value="date">Date</option>
            <option value="occupied">Occupancy</option>
            <option value="weight">Weight</option>
          </select>
          <br>
          <br>
          <p class="filter-title">Sorting order</p>
          <select v-model="sorting_order_input" name="sorting-order-form">
            <option value="asc">Ascending</option>
            <option value="desc">Descending</option>
          </select>
        </div>
        <div class="filter-div">
          <p class="filter-title">Type</p>
          <select id="filter" v-model="type_input" name="filtering-form">
            <option value="all">All</option>
            <option value="CHEST_MACHINE">Chest machine</option>
            <option value="BICEPS_MACHINE">Biceps machine</option>
            <option value="TREADMILL">Treadmill</option>
          </select>
          <br>
          <br>
          <p class="filter-title">Data format</p>
          <select v-model="format_input" name="format-filter-form">
            <option value="application/json">JSON</option>
            <option value="text/csv">CSV</option>
          </select>
        </div>
        <div class="filter-div">
          <p class="filter-title">From</p>
          <input v-model="date_start_input" max="12-31-2021"
                 min="01-01-2021" name="date-start-filter-form" type="date">
          <br>
          <br>
          <p class="filter-title">To the</p>
          <input v-model="date_end_input" max="01-01-2022"
                 min="01-02-2021" name="date-end-filter-form" type="date">
        </div>
        <div class="filter-div">
          <p class="filter-title">Occupancy</p>
          <select v-model="occupancy_input" name="occupancy-filter-form">
            <option value="all">All</option>
            <option value="true">True</option>
            <option value="false">False</option>
          </select>
          <br>
          <br>
          <p class="filter-title">Weight</p>
          <input v-model="weight_input" min="0" name="weight-filter-form" step="1" type="number">
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
      <BarChart :chartData="bar_chart_data"/>
      <br>
      <Doughnut_chart :chartData="doughnut_chart_data"/>
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

<script lang="ts" setup>
import {NAV_CHARTS_TITLE, NAV_DASHBOARD_TITLE, NAV_TABLE_TITLE, NAV_TITLE, TABLE_TITLE} from "@/constants/texts";
import {onMounted, ref, watch} from 'vue'
import {get_sensor_data_from_api} from "@/controller/sensor_data";
import {get_dashboard_data_from_api} from "@/controller/dashboard";
import {prepareBarChartData, prepareDoughNutChartData} from "@/controller/charts";
import {useInterval} from "@vueuse/core";
import {collect_parameters, parse_parameters} from "@/controller/parameters";
import BarChart from "@/components/bar_chart.vue";
import Doughnut_chart from "@/components/doughnut_chart.vue";

let rows = ref()
let sort_input = ref("id")
let type_input = ref("all")
let date_start_input = ref(undefined)
let date_end_input = ref(undefined)
let occupancy_input = ref("all")
let weight_input = ref("")
let format_input = ref("application/json")
let sorting_order_input = ref("asc")
let dashboard_data = ref()
let bar_chart_data = ref({
  labels: [],
  datasets: []
})
let doughnut_chart_data = ref({
  labels: ['VueJs', 'EmberJs', 'ReactJs', 'AngularJs'],
  datasets: [
    {
      backgroundColor: ['#41B883', '#E46651', '#00D8FF', '#DD1B16'],
      data: [40, 20, 80, 10]
    }
  ]
})

const {counter, pause, resume} = useInterval(1000, {controls: true})
watch(counter, async () => {
  pause()
  let url_parameters: string[] = collect_parameters(sort_input.value, type_input.value, occupancy_input.value, weight_input.value, sorting_order_input.value, date_start_input.value, date_end_input.value)
  dashboard_data.value = await get_dashboard_data_from_api()
  rows.value = await get_sensor_data_from_api(format_input.value, parse_parameters(url_parameters))
  bar_chart_data.value = prepareBarChartData(rows.value)
  doughnut_chart_data.value = prepareDoughNutChartData(rows.value)
  resume()
})


function scrollToElement(id: string) {
  const element = document.getElementById(id);
  element!.scrollIntoView({
    behavior: "smooth"
  });
}

onMounted(async () => {
  rows.value = await get_sensor_data_from_api(format_input.value, parse_parameters([]))
  bar_chart_data.value = {labels: [], datasets: []}
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

.dashboard-wrapper {
  display: flex;
  justify-content: center;
  align-items: center;
  padding-top: 5px;
}

.dashboard-container {
  background-color: #00C7FD;
  width: 200px;
  align-items: center;
  justify-content: center;
  padding: 10px;
  border-radius: 15px;
}

.dashboard-container-text {
  color: #2E2F32;
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
  color: #2E2F32;
  border-radius: 5px;
}

input {
  background-color: #00C7FD;
  font-weight: bold;
  color: #2E2F32;
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
  color: #2E2F32;
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
  height: 1200px;
}

/*Dashboard section*/
.dashboard-section {
  height: 400px;
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
