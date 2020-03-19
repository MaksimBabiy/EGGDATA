import { axios } from 'core';
export default {
   get: () => axios.get(`http://localhost:56839/api/AdminDoctors/Get`),
   add: data => axios.post(`http://localhost:56839/api/AdminDoctors/AddDoctor`, data),
   update: data => axios.patch(`http://localhost:56839/api/AdminDoctors/UpdateDoctor`,data),
   delete: id => axios.delete(`http://localhost:56839/api/AdminDoctors/DeleteDoctor/${id}`)
}