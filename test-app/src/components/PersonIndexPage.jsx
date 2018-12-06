import React from 'react';
import http from 'axios';
import { NavLink } from 'react-router-dom';
import moment from 'moment';


class PersonIndexPage extends React.Component {
    state = {
        listofPersons: []
    }

    baseUrl = "http://localhost:5000/api/persons";

    async componentDidMount() {

        const response = await http.get(`${this.baseUrl}`);
        if (response.status === 200) {
            const listofPersons = response.data;
            console.log(listofPersons);

            this.setState({ listofPersons });
        }
    }

    handleDelete = async (id) => {
        if (window.confirm('Are you sure to delete!')) {
            const response = await http.delete(`${this.baseUrl}/${id}`);
            if (response.status === 200) {
                const { listofPersons } = this.state;
                const index = listofPersons.findIndex(people => people.id === id);
                if (index > -1) {
                    listofPersons.splice(index, 1);
                    this.setState({ listofPersons });
                }

            }
        }
    }

    render() {
        const { listofPersons } = this.state;
        return (
            <div className=" col-sm-12">
                <table className="table table-bordered">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Date of Birth</th>
                            <th>Mobile No.</th>
                            <th>Division</th>
                            <th>District</th>
                            <th>Upazilla</th>
                            <th>Village</th>
                            <th>Date</th>

                            <th width="210px">Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        {listofPersons.map((person, index) => (
                            <tr key={index}>
                                <td>{person.name}</td>
                                <td>{moment(person.dob).format("DD-MM-YYYY")}</td>
                                <td>{person.mobileNo}</td>
                                <td>{person.divisionName}</td>
                                <td>{person.districtName}</td>
                                <td>{person.upazillaName}</td>
                                <td>{person.villageName}</td>
                                <td>{moment(person.createdAt).format("DD-MM-YYYY hh:mm:ss a")}</td>
                                <td>
                                    <NavLink to={`/person/edit/${person.id}`} className="btn btn-sm btn-warning ml-2">Edit</NavLink>
                                    <button className="btn btn-sm btn-danger ml-2" onClick={() => this.handleDelete(person.id)}>Delete</button>
                                    <NavLink to={`/person/detail/${person.id}`} className="btn btn-sm btn-info ml-2">Details</NavLink>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        );
    }
}
export default PersonIndexPage;