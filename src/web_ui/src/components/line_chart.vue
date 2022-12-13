<template>
  <div class="chart-wrapper">
    <div class="chart-color">
      <Line :data="chartData" :style="myStyles"/>
    </div>

  </div>
</template>

<script lang="ts">
import {CategoryScale, Chart as ChartJS, Legend, LinearScale, LineElement, PointElement, Title, Tooltip} from 'chart.js'
import {Line} from 'vue-chartjs'

ChartJS.register(
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip,
    Legend
)

export default {
  name: 'LineChart',
  components: {Line},
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
    }
  },
}
</script>

<style scoped>

.chart-wrapper {
  display: flex;
  align-items: center;
  justify-content: center;
}

.chart-color {
  width: 750px;
  background-color: #2E2F32 !important;
  display: flex;
  align-items: center;
  justify-content: center;
}

</style>
