import React from 'react';
import Toolbar, { Item } from 'devextreme-react/toolbar';
import Button from 'devextreme-react/button';
import './Header.scss';
import { Template } from 'devextreme-react/core/template';
import type { HeaderProps } from '../../types';
import UserPanel from "../user-panel/UserPanel";

export default function Header({ menuToggleEnabled, title, toggleMenu }: HeaderProps) {
  return (
    <header className={'header-component'}>
      <Toolbar className={'header-toolbar'}>
        <Item
          visible={menuToggleEnabled}
          location={'before'}
          widget={'dxButton'}
          cssClass={'menu-button'}
        >
          <Button icon="menu" stylingMode="text" onClick={toggleMenu} />
        </Item>
        <Item
          location={'before'}
          cssClass={'header-title'}
          text={title}
          visible={!!title}
        />
        <Item
            location={'after'}
            locateInMenu={'auto'}
            menuItemTemplate={'userPanelTemplate'}
        >
          <Button
              className={'user-button authorization'}
              width={210}
              height={'100%'}
              stylingMode={'text'}
          >
            <UserPanel menuMode={'context'} />
          </Button>
        </Item>
        <Template name={'userPanelTemplate'}>
          <UserPanel menuMode={'list'} />
        </Template>
      </Toolbar>
    </header>
)}