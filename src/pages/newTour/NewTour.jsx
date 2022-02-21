import "./newTour.css";

export default function NewTour() {
  return (
    <div className="newTour">
      <h1 className="addTourTitle">New Tour</h1>
      <form className="addTourForm">
        <div className="addTourItem">
          <label>Image</label>
          <input type="file" id="file" />
        </div>
        <div className="addTourItem">
          <label>Name Tour</label>
          <input type="text" placeholder="Du Lịch Đà Nẵng" />
        </div>
        <div className="addTourItem">
          <label>lộ trình </label>
          <input type="text" placeholder="1.254" />
        </div>
        <div className="addTourItem">
          <label>Active</label>
          <select name="active" id="active">
            <option value="yes">Yes</option>
            <option value="no">No</option>
          </select>
        </div>
        <button className="addTourButton">Create</button>
      </form>
    </div>
  );
}
