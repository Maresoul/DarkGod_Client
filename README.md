# DarkGod_Client

Unity开发的3D-ARPG俯视角网络游戏，完整的客户端+服务端架构，实现常见的游戏业务系统和战斗模块。

● ***gif形式展示，如果未显示稍等一段时间加载***

● *完整项目演示地址：*[项目演示地址](https://www.bilibili.com/video/BV1MpTkeCEWe/?spm_id_from=333.999.0.0&vd_source=15ce64d8f8fad36086523ce711dec730)

## 项目架构

- **客户端**: 基于Unity3D引擎开发，使用C#编写游戏逻辑。
- **服务端**: 基于PESocket库的服务器开发，数据库使用MySQL，支持多玩家并发处理。
- **网络通信**: 使用WebSocket协议实现客户端与服务端之间的实时通信

## UI逻辑框架

![UI逻辑框架](https://github.com/Maresoul/DarkGod_Client/blob/main/DisplayResources/UI%E9%80%BB%E8%BE%91%E6%A1%86%E6%9E%B6.png)

## 服务端逻辑

![服务端逻辑](https://github.com/Maresoul/DarkGod_Client/blob/main/DisplayResources/%E6%9C%8D%E5%8A%A1%E7%AB%AF%E6%A1%86%E6%9E%B6.png)

## 战斗逻辑框架

![战斗逻辑框架](https://github.com/Maresoul/DarkGod_Client/blob/main/DisplayResources/%E6%88%98%E6%96%97%E9%80%BB%E8%BE%91%E6%A1%86%E6%9E%B6.png)

## 功能演示

1. **登陆场景和角色创建(服务器端和数据库)**
   ![登陆场景和角色创建](https://github.com/Maresoul/DarkGod_Client/blob/main/DisplayResources/out_1723785667629.gif)
2. **界面加载(异步)**
   ![界面加载](https://github.com/Maresoul/DarkGod_Client/blob/main/DisplayResources/tinywow_%E5%9C%BA%E6%99%AF%E5%8A%A0%E8%BD%BD_62451080.gif)
3. **Tip动态提示**
   ![Tip动态提示](https://github.com/Maresoul/DarkGod_Client/blob/main/DisplayResources/tinywow_tips%E6%8F%90%E7%A4%BA_62451171.gif)
4. **玩家移动和主城UI**
   ![玩家移动和主城UI](https://github.com/Maresoul/DarkGod_Client/blob/main/DisplayResources/tinywow_%E7%8E%A9%E5%AE%B6%E7%A7%BB%E5%8A%A8%E5%92%8C%E4%B8%BB%E5%9F%8EUI_62451239.gif)
5. **角色信息展示面板**
   ![角色信息展示面板](https://github.com/Maresoul/DarkGod_Client/blob/main/DisplayResources/tinywow_%E8%A7%92%E8%89%B2%E4%BF%A1%E6%81%AF%E5%B1%95%E7%A4%BA%E9%9D%A2%E6%9D%BF_62451317.gif)
6. **任务引导（Navigator自动寻路）**
   ![任务引导](https://github.com/Maresoul/DarkGod_Client/blob/main/DisplayResources/tinywow_%E8%87%AA%E5%8A%A8%E4%BB%BB%E5%8A%A1%E5%AF%BB%E8%B7%AF_62451372.gif)
7. **NPC对话**
   ![NPC对话](https://github.com/Maresoul/DarkGod_Client/blob/main/DisplayResources/tinywow_NPC%E5%AF%B9%E8%AF%9D_62451455.gif)
8. **角色强化升级**
   ![角色强化升级](https://github.com/Maresoul/DarkGod_Client/blob/main/DisplayResources/tinywow_%E8%A7%92%E8%89%B2%E5%BC%BA%E5%8C%96%E5%8D%87%E7%BA%A7_62451495.gif)
9. **铸造**
   ![铸造](https://github.com/Maresoul/DarkGod_Client/blob/main/DisplayResources/tinywow_%E9%93%B8%E9%80%A0%E7%B3%BB%E7%BB%9F_62451583.gif)
10. **任务系统**
    ![任务系统](https://github.com/Maresoul/DarkGod_Client/blob/main/DisplayResources/tinywow_%E4%BB%BB%E5%8A%A1%E7%B3%BB%E7%BB%9F_62451618.gif)
11. **世界聊天**
    ![世界聊天](https://github.com/Maresoul/DarkGod_Client/blob/main/DisplayResources/tinywow_%E4%B8%96%E7%95%8C%E8%81%8A%E5%A4%A9_62451648.gif)
12. **离线角色体力恢复**
    ![定时体力恢复系统](https://github.com/Maresoul/DarkGod_Client/blob/main/DisplayResources/tinywow_%E7%A6%BB%E7%BA%BF%E4%BD%93%E5%8A%9B%E5%9B%9E%E5%A4%8D_62451676.gif)
13. **副本角色控制**
    ![角色状态控制](https://github.com/Maresoul/DarkGod_Client/blob/main/DisplayResources/tinywow_%E5%89%AF%E6%9C%AC%E8%A7%92%E8%89%B2%E6%8E%A7%E5%88%B6_62451732.gif)
14. **技能表现（CD,位移，动画播放）**
    ![技能表现](https://github.com/Maresoul/DarkGod_Client/blob/main/DisplayResources/tinywow_%E6%8A%80%E8%83%BD%E8%A1%A8%E7%8E%B0_62451780.gif)
15. **普攻连招**
    ![普攻连招](https://github.com/Maresoul/DarkGod_Client/blob/main/DisplayResources/tinywow_%E6%99%AE%E6%94%BB%E8%BF%9E%E6%8B%9B_62451857.gif)
16. **怪物AI表现**
    ![怪物AI表现](https://github.com/Maresoul/DarkGod_Client/blob/main/DisplayResources/tinywow_%E6%80%AA%E7%89%A9AI_62451892.gif)
17. **攻击与受击，血条控制，UI飘字**
    ![攻击与受击1](https://github.com/Maresoul/DarkGod_Client/blob/main/DisplayResources/tinywow_%E6%94%BB%E5%87%BB%E4%B8%8E%E5%8F%97%E5%87%BB_1_62451941.gif)
    ![攻击与受击2](https://github.com/Maresoul/DarkGod_Client/blob/main/DisplayResources/tinywow_%E6%94%BB%E5%87%BB%E4%B8%8E%E5%8F%97%E5%87%BB_2_62451982.gif)
18. **战斗失败**
    ![战斗失败](https://github.com/Maresoul/DarkGod_Client/blob/main/DisplayResources/tinywow_%E6%88%98%E6%96%97%E5%A4%B1%E8%B4%A5_62452033.gif)
19. **战斗成功结算界面**
    ![战斗成功结算界面](https://github.com/Maresoul/DarkGod_Client/blob/main/DisplayResources/tinywow_%E6%88%98%E6%96%97%E8%83%9C%E5%88%A9_62452079.gif)
