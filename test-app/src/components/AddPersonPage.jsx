import React from 'react';
import http from 'axios';


class AddPersonPage extends React.Component {

  state = {
    person: {
      name: '',
      dob: '',
      mobileNo: '',
      divisionId: '',
      districtId: '',
      upazillaId: '',
      villageName: ''
    },
    listofDivisions: [],
    listofDistricts:[],
    listofUpazillas:[]
  };

  handleChange = (event) => {
    const { name, value } = event.target;

    this.setState((prevState) => ({
      person: {
        ...prevState.person, [name]: value
      }
    }));
  };

  baseUrl = "http://localhost:5000/api";

  async componentDidMount() {
    const response = await http.get(`${this.baseUrl}/divisions`);
    if (response.status === 200) { 
      const listofDivisions = response.data;
      this.setState({ listofDivisions });
    }
  };


  handleChangeDivision = async event => {
    const { value } = event.target; 
    this.setState( (prevState)=>({
      person:{
        ...prevState.person, divisionId:value
      }
    }));
    if (!value) return;

    const response = await http.get(
      `${this.baseUrl}/districts/id/${value}`);  
      
    const listofDistricts = response.data || [];
    this.setState({ listofDistricts });
  };



  handleChangeDistrict= async event =>{
 const { value } = event.target; 
    this.setState( (prevState)=>({
      person:{
        ...prevState.person, districtId:value
      }
    }));
    if (!value) return;

    const response = await http.get(
      `${this.baseUrl}/upazillas/id/${value}`);

      console.log(response.data);
      
     
    const listofUpazillas = response.data || [];
    this.setState({ listofUpazillas });
  };


  handleSubmit = async (event) => {
    event.preventDefault();
    const {  person } = this.state;
    const response = await http.post(`${this.baseUrl}/persons`, person);
    if (response.status === 201) {
      console.log(response.data);

      this.props.history.push('/PersonIndexPage'); //redirect to another page

    } 
  };





  render() {

    const { person: { name, dob, mobileNo, divisionId, districtId, upazillaId, villageName }, listofDivisions,listofDistricts,listofUpazillas} = this.state;
    return (
      <div className="card-body border minHeight">
        <div className="offset-2 col-sm-8">
          <form onSubmit={this.handleSubmit}>
            <div className="form-group row">
              <label htmlFor="name" className="col-sm-4 col-form-label">
                Name
              </label>
              <div className="col-sm-8">
                <input
                  type="text"
                  className="form-control"
                  id="name"
                  name="name"
                  value={name}
                  placeholder=""
                  onChange={this.handleChange}
                />
              </div>
            </div>
            <div className="form-group row">
              <label htmlFor="dob" className="col-sm-4 col-form-label">
                Date of Birth
              </label>
              <div className="col-sm-8">
                <input
                  type="date"
                  className="form-control"
                  id="dob"
                  name="dob"
                  value={dob}
                  placeholder=""
                  onChange={this.handleChange}
                />
              </div>
            </div>
            <div className="form-group row">
              <label htmlFor="mobileNo" className="col-sm-4 col-form-label">
                Mobile No.
              </label>
              <div className="col-sm-8">
                <input
                  type="text"
                  className="form-control"
                  id="mobileNo"
                  name="mobileNo"
                  value={mobileNo}
                  placeholder=""
                  onChange={this.handleChange}
                />
              </div>
            </div>
            <div className="form-group row">
              <label htmlFor="divisionId" className="col-sm-4 col-form-label">
                Division
              </label>
              <div className="col-sm-8">
                <select name="divisionId" id="divisionId" className="form-control" value={divisionId} onChange={this.handleChangeDivision} >
                  <option className="textDesign">--Select--</option>
                  {listofDivisions.map((division, index) => (
                    <option  key={index} value={division.id}> {division.name}</option>
                  ))}
                </select>
              </div>
            </div>
            <div className="form-group row">
              <label htmlFor="districtId" className="col-sm-4 col-form-label">
                District
              </label>
              <div className="col-sm-8">
                <select name="districtId" id="districtId" className="form-control" value={districtId} onChange={this.handleChangeDistrict} >
                  <option>--Select--</option>
                  {listofDistricts.map((district, index) => (
                    <option value={district.id} key={index}> {district.name}</option>
                  ))}

                </select>
              </div>
            </div>
            <div className="form-group row">
              <label htmlFor="upazillaId" className="col-sm-4 col-form-label">
                Upazilla
              </label>
              <div className="col-sm-8">
                <select name="upazillaId" id="upazillaId" className="form-control" value={upazillaId} onChange={this.handleChange} >
                  <option>--Select--</option>
                  {listofUpazillas.map((upazilla, index) => (
                    <option value={upazilla.id} key={index}> {upazilla.name}</option>
                  ))}
                </select>
              </div>
            </div>
            <div className="form-group row">
              <label htmlFor="villageName" className="col-sm-4 col-form-label">
                VillageName
              </label>
              <div className="col-sm-8">
                <input
                  type="text"
                  className="form-control"
                  id="villageName"
                  name="villageName"
                  value={villageName}
                  placeholder=""
                  onChange={this.handleChange}
                />
              </div>
            </div>
            <div className="form-group row">
              <div className="col-sm-4" />
              <div className="col-sm-8">
                <button className="btn  btn-primary" type="submit">
                  Submit
                </button>
              </div>
            </div>
          </form>
        </div>
      </div>
    );
  }
}
export default AddPersonPage;