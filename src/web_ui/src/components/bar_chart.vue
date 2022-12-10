<template>
  <div class="chart-wrapper">
    <Bar :data="chartData" :style="myStyles"/>
  </div>

</template>

<script>
import {Bar} from 'vue-chartjs'
import {BarElement, CategoryScale, Chart as ChartJS, Legend, LinearScale, Title, Tooltip} from 'chart.js'

ChartJS.register(Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale)


export default {
  name: 'BarChart',
  components: {Bar},
  computed: {
    myStyles() {
      return {
        height: '400px',
        width: '750px',
      }
    }
  },
  props: {
    chartData: {
      type: Object,
      required: true
    },
    chartOptions: {
      type: Object,
      default: () => {
      }
    },
  },
}
</script>

<style scoped>

.chart-wrapper {
  display: flex;
  align-items: center;
  justify-content: center;
}

</style>
