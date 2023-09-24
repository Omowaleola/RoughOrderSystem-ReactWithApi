import React, {useEffect, useState} from "react";
import {OrderEditingModel, OrderProductModel, OrderSavingModel, OrderViewModel} from "../../models/order-view-model";
import {
    ClearSavedOrderProducts,
    GetOrder,
    GetOrders,
    GetSavedOrderProducts,
    InsertOrder,
    UpdateOrder
} from "../../api/orders";
import {Button, Column, Editing, Lookup} from "devextreme-react/data-grid";
import {DataGrid} from "devextreme-react";
import {ProductViewModel} from "../../models/product-view-model";
import {useAuth} from "../../contexts/auth";
import {GetOrderRequestModel} from "../../models/get-order-request-model";
import {UserType} from "../../models/user-type";
import {GetProducts, UpdateProduct} from "../../api/products";
import notify from "devextreme/ui/notify";
import {useLocation, useNavigate} from "react-router-dom";
import LoadPanel from "devextreme-react/load-panel";

export default function AddOrder() {
    const [products, setProducts] = useState<ProductViewModel[]>();
    const [orderToEdit, setOrderToEdit] = useState<OrderViewModel>(null);
    const [orderProducts, setOrderProducts] = useState<OrderProductModel[]>(GetSavedOrderProducts());
    const [loading, setLoading] = useState(true);
    const [isEdit, setIsEdit] = useState(false);
    const [isAdd, setIsAdd] = useState(false);
    const [isView, setIsView] = useState(false);
    const {user} = useAuth();
    const navigate = useNavigate();
    const location = useLocation();
    
    
    const CreateOrder = async () => {
        setLoading(true);
        const order: OrderSavingModel =
            {
                customerId: user?.id,
                products: orderProducts
            };
        const result = await InsertOrder(order);
        if (result.isOk) {
            ClearSavedOrderProducts();
            setLoading(false);
            navigate('/orders')
            notify('Order Added Successfully', 'success', 3000);
        } else {
            notify(result.data, 'error', 2000);
        }
        setLoading(false);
    }
    const formatDate = (dateExt) => {
        const date = new Date(dateExt);
        const year = date.getFullYear();
        const month = String(date.getMonth() + 1).padStart(2, '0');
        const day = String(date.getDate()).padStart(2, '0');
        const hours = String(date.getHours()).padStart(2, '0');
        const minutes = String(date.getMinutes()).padStart(2, '0');
        const seconds = String(date.getSeconds()).padStart(2, '0');

        return `${year}/${month}/${day} ${hours}:${minutes}`;
    };
    const EditOrder = async  () =>
    {
        setLoading(true);
        const editOrder: OrderEditingModel =
            {
                OrderId : orderToEdit.id,
                customerId : user?.id,
                products: orderToEdit.products
            };
        const result = await UpdateOrder(editOrder);
        if (result.isOk)
        {
            setLoading(false);
            navigate('/orders')
            notify('Order Edited Successfully', 'success', 3000);
            
        }else {
            setLoading(false);
            notify(result.data, 'error', 3000);
        }
        
    }
    useEffect(() => {
        (async function () {
            const getResult = await GetProducts();
            if (getResult.isOk) {
                setProducts(getResult.data);
                setIsEdit(location.state.isEdit);
                setIsView(location.state.isView);
                setIsAdd(location.state.isAdd);                
                if(location.state.isEdit ||location.state.isView)
                {
                    const result = await  GetOrder(location.state.orderId);
                    if(result.isOk)
                    {
                        console.log(orderToEdit);
                        const order: OrderViewModel = result.data;
                        setOrderToEdit(order);
                    }
                }
            } else {
                notify(getResult.data, 'error', 2000);
            }
            setLoading(false);
        })();
    }, [GetOrder, GetProducts,  ]);
    return (
        <React.Fragment>
            <LoadPanel
                visible={loading}
            />
            {(isEdit) && <h6>Edit Order</h6>}
            {isAdd && <h6> Add Order</h6>}
            {isView && <h6>View Order</h6>}
            {(isEdit || isView) &&
                <div>
                    <p><b>Order No: </b> {orderToEdit?.orderNo}</p>
                    <p><b>{orderToEdit?.paid ?'Paid': 'Not Paid' }</b></p>
                    <p><b>CreatedOn </b> {formatDate(orderToEdit?.createdOn)}</p>
                    {isView&&<p><b>Total: </b> {orderToEdit?.total}</p>}
                    {(user?.userRole== UserType.admin)&&<p><b>Customer </b> {orderToEdit?.customer}</p>}
                </div>}
            <DataGrid
                dataSource={(isEdit || isView)? orderToEdit?.products :orderProducts}
                keyExpr="id">
                <Editing
                    mode="form"
                    allowAdding={isEdit ||isAdd}
                    allowUpdating={isEdit ||isAdd}
                    allowDeleting={isEdit ||isAdd}
                />
                <Column dataField="productId" caption="Product" allowEditing={true}>
                    <Lookup
                        dataSource={products}
                        valueExpr="id"
                        displayExpr="name"/>
                </Column>
                <Column dataField="quantity" dataType="number" allowEditing={true}/>
                <Column type="buttons">
                    <Button name="edit" icon="edit"/>
                    <Button name="delete" icon="trash"/>
                </Column>
            </DataGrid>

            {isAdd && <input type="button" value="Create Order" onClick={CreateOrder}/>}
            {isEdit && <input type="button" value="Edit Order" onClick={EditOrder}/>}


        </React.Fragment>

    )
}