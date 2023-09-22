import renderer from "react-test-renderer";
import {DataGrid} from "devextreme-react";
import React, {useEffect, useState} from "react";
import {OrderGridViewModel} from "../../models/order-view-model";
import {Button, Column} from "devextreme-react/data-grid";
import {UserType} from "../../models/user-type";
import {useAuth} from "../../contexts/auth";
import notify from "devextreme/ui/notify";
import {GetOrder, GetOrders} from "../../api/orders";
import {GetOrderRequestModel} from "../../models/get-order-request-model";
import LoadPanel from "devextreme-react/load-panel";
import {useNavigate} from "react-router-dom";
import Orders from "./orders";

jest.mock("devextreme-react");
jest.mock("../../models/order-view-model");
jest.mock("devextreme-react/data-grid");
jest.mock("../../models/user-type");
jest.mock("../../contexts/auth");
jest.mock("devextreme/ui/notify");
jest.mock("../../api/orders");
jest.mock("../../models/get-order-request-model");
jest.mock("devextreme-react/load-panel");
jest.mock("react-router-dom");

const renderTree = tree => renderer.create(tree);
describe('<Orders>', () => {
  it('should render component', () => {
    expect(renderTree(<Orders 
    />).toJSON()).toMatchSnapshot();
  });
  
});