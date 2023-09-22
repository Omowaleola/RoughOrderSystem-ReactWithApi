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

export default function Orders() {
    const navigate = useNavigate();
    const [orders, setOrders] = useState<OrderGridViewModel[]>();
    const [loading, setLoading] = useState(true);
    const {user} = useAuth();
    const EditOrder = async (e) => {
        const result = await GetOrder(e.row.key);
        if (result.isOk) {
            navigate(`/order`, {
                state: {
                    isEdit: true,
                    isView: false,
                    isAdd: false,
                    orderId: e.row.key
                }
            })
        } else {
            notify(result.data, 'error', 2000);
        }
    }
    const ViewOrder = async (e) => {

        const result = await GetOrder(e.row.key);
        if (result.isOk) {
            navigate(`/order`, {
                state: {
                    isEdit: false,
                    isView: true,
                    isAdd: false,
                    orderId: result.data.id
                }
            })
        } else {
            notify(result.data, 'error', 2000);
        }
    }

    const canEdit =
        (e) => {
            return !e.row.data.paid;
        }

    const canView =
        (e) => {
            return e.row.data.paid;
            //return  !canEdit(e);
        }

    useEffect(() => {
        (async function () {
            const request: GetOrderRequestModel = {
                customerId: user?.userRole === UserType.customer ? user?.id : ''
            };
            const result = await GetOrders(request);
            if (result.isOk) {
                setOrders(result.data);
            } else {
                notify(result.data, 'error', 2000);
            }
            setLoading(false);
        })();
    }, [user]);

    return (
        <React.Fragment>
            <h2>Orders</h2>
            <LoadPanel
                visible={loading}
            />
            <DataGrid
                dataSource={orders}
                keyExpr="id">
                <Column dataField="orderNo" allowEditing={false}/>
                <Column dataField="paid" allowEditing={false}/>
                <Column dataField="total" dataType="number"
                        format={{
                            type: 'currency',
                            currency: "ZAR",
                            precision: 2
                        }} 
                        allowEditing={false}/>
                <Column dataField="createdOn" dataType={"date"} allowEditing={false} format={"yyyy/MM/dd HH:mm"}/>
                <Column dataField="customer" visible={user?.userRole === UserType.admin} allowEditing={false}/>
                <Column type="buttons">
                    <Button name="edit" icon="edit" visible={canEdit} onClick={EditOrder}/>
                    <Button icon="eyeopen" visible={canView} onClick={ViewOrder}/>
                </Column>
            </DataGrid>


        </React.Fragment>
    )
}