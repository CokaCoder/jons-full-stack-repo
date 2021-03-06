
const ReceivedMessages = { template: `<h1>Inbox</h1>

    <table class="table table-striped">
        <thead>
            <tr>
                <th>
                    Content
                </th>
                <th>
                    Date Received
                </th>
            </tr>
            <tbody>
                <tr v-for="message in ReceivedMessages">
                    <td>{{message.Content}}</td>
                    <td>{{message.DateReceived}}</td>
                    <td>
                        <button type="button" class="btn btn-light mr-1">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16">
                            <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
                            <path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z"/>
                            </svg>
                        </button>
                        <button type="button" class="btn btn-light mr-1">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash" viewBox="0 0 16 16">
                            <path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0V6z"/>
                            </svg>
                        </button>
                    </td>
                </tr>
            </tbody>
        </thead>
    </table>


<div class="modal fade" id="exampleModal" tabinded="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg modal-dialog-centered">
        <div class="modal-content">
            <div class="modadl-header">
                <!-- <h5 class="Modal-title" id="exampleModalLabel">{{modalTitle}}</h5> -->
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
        </div>
    </div>  
</div>



`,


    //define an array to hold the returned json
    data(){
        return{
            ReceivedMessages: []
        }
    },
    methods:{
        refreshData(){
           
            // axios.get(variables.API_URL+"ReceivedMessages")
            // .then((response) => {
            //     this.ReceivedMessages=response.data; //puts the returned result into the array
            // });

            axios
            .get(variables.API_URL+"ReceivedMessages", { 
                headers: {
                    'Access-Control-Allow-Origin' : '*',
                    'Access-Control-Allow-Methods':'GET,PUT,POST,DELETE,PATCH,OPTIONS',
                }
            })
            .then((response) => {
                this.ReceivedMessages=response.data; //puts the returned result into the array
             })
            .catch((error) => {
                console.log('error ' + error);
             });

        }
    },
    mounted: function(){
        this.refreshData();
    }
}