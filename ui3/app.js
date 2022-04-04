// Define route components.
//const ReceivedMessages = { template: '<h1>This is Received SMS Messages</h1>' }
//const SentMessages = { template: '<h1>This is Sent SMS Messages</h1>' }
//const Home = { template: '<h1>This is Home</h1>' }

// Define routes
const routes = [
    { path: '/SentMessages', component: SentMessages},
    { path: '/ReceivedMessages', component: ReceivedMessages},
    { path: '/Home', component: Home},
    { path: '/', component: Home}
]

// Create the router instance and pass the `routes` option
const router = VueRouter.createRouter({
  history: VueRouter.createWebHashHistory(),
  routes
})

// Create and mount root instance.
const app = Vue.createApp({})
app.use(router)
app.mount('#app')