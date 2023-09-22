import React, {useState} from 'react';
import Toolbar, {Item} from 'devextreme-react/toolbar';
import Button from 'devextreme-react/button';
import './Header.scss';
import {Template} from 'devextreme-react/core/template';
import type {HeaderProps} from '../../types';
import UserPanel from "../user-panel/UserPanel";
import {HasSavedOrderProducts} from "../../api/orders";
import notify from 'devextreme/ui/notify';
import {useNavigate} from "react-router-dom";

export default function Header({menuToggleEnabled, title, toggleMenu}: HeaderProps) {
    const navigate = useNavigate();
    const CreateOrder = () => {
        if (HasSavedOrderProducts()) {
            navigate('/addOrder',
                {
                    state: {
                        isEdit: false,
                        isView: false,
                        isAdd: true
                    }
                })
        } else {
            navigate('/home')
            notify('No products selected', 'error', 3000);
        }
    }
    return (
        <header className={'header-component'}>
            <Toolbar className={'header-toolbar'}>
                <Item
                    visible={menuToggleEnabled}
                    location={'before'}
                    widget={'dxButton'}
                    cssClass={'menu-button'}
                >
                    <Button icon="menu" stylingMode="text" onClick={toggleMenu}/>
                </Item>
                <Item
                    location={'before'}
                    cssClass={'header-title'}
                    text={title}
                    visible={!!title}
                />
                <Item
                    location={'after'}
                    widget={'dxButton'}>
                    <Button icon="cart" hint="Create Order" stylingMode="outlined" onClick={CreateOrder}/>
                </Item>
                <Item
                    location={'after'}
                    locateInMenu={'auto'}
                    menuItemTemplate={'userPanelTemplate'}
                >
                    <Button
                        className={'user-button authorization'}
                        width={100}
                        height={'100%'}
                        stylingMode={'text'}
                    >
                        <UserPanel menuMode={'context'}/>
                    </Button>
                </Item>
                <Template name={'userPanelTemplate'}>
                    <UserPanel menuMode={'list'}/>
                </Template>
            </Toolbar>
        </header>
    )
}
