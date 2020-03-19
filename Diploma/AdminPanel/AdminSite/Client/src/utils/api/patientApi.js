import { axios } from 'core';
export default {
   get: () => axios.get(`http://localhost:56839/api/AdminPatients/Get`),
   add: data => axios.post(`http://localhost:56839/api/AdminPatients/AddPatient`, data),
   update: data => axios.patch(`http://localhost:56839/api/AdminPatients/UpdatePatient`,data),
   delete: id => axios.delete(`http://localhost:56839/api/AdminPatients/DeletePatient/${id}`)
}