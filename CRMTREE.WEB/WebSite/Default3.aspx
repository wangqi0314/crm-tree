<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default3.aspx.cs" Inherits="Default3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%--<link href="/Semantic/packaged/css/semantic.css" rel="stylesheet" />--%>
    <link href="/css/semantic.css" rel="stylesheet" />
    <%--   <script src="/Scripts/jquery-1.8.2.min.js"></script>
    <script src="/Semantic/packaged/javascript/semantic.js"></script>--%>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="ui animated button">
                <div class="visible content">Next</div>
                <div class="hidden content">
                    <i class="right arrow icon"></i>
                </div>
            </div>
            <div class="ui vertical animated button">
                <div class="hidden content">Shop</div>
                <div class="visible content">
                    <i class="cart icon"></i>
                </div>
            </div>
            <div class="ui animated fade button">
                <div class="visible content">Sign-up for a Pro account</div>
                <div class="hidden content">
                    $12.99 a month
                </div>
            </div>
        </div>
        <div>
            <div class="ui buttons">
                <div class="ui button">Cancel</div>
                <div class="or"></div>
                <div class="ui positive button">Save</div>
            </div>
        </div>
        <div>
            <div class="ui buttons">
                <div class="ui positive button">Cancel</div>
                <div class="or"></div>
                <div class="ui  button">Save</div>
            </div>
        </div>
        <div class="large ui buttons">
            <div class="ui button">Cancel</div>
            <div class="ui button">Continue</div>
        </div>
        <div class="large ui buttons">
            <div class="ui button">Cancel</div>
            <%--<div class="ui button">Continue</div>--%>
        </div>

        <div class="ui labeled icon button">
            Download <i class="download icon"></i>
        </div>
        <div class="ui icon button">
            <i class="download icon"></i>
        </div>
        <div class="ui button red">
            Download
        </div>
        <div class="ui facebook button">
            <i class="facebook icon"></i>
            Facebook
        </div>


        <div>

            <div class="ui icon menu">
                <a class="item">
                    <i class="mail icon"></i>
                </a>
                <a class="item">
                    <i class="lab icon"></i>
                </a>
                <a class="item">
                    <i class="star icon"></i>
                </a>
            </div>
        </div>
        <div>
            <div class="ui tiered menu red">
                <div class="menu ">
                    <div class="active item">
                        <i class="home icon"></i>
                        Home
                    </div>
                    <a class="item red">
                        <i class="mail icon"></i>
                        Mail
                       <span class="ui label">22</span>
                    </a>
                </div>
                <div class="sub menu ">
                    <div class="active item">Activity</div>
                    <a class="item">Profile</a>
                </div>
            </div>
        </div>
        <div style="height: 30px;">
            <div class="ui vertical divider red" style="height: 50px; width: 300px;">
                Or
            </div>
            <div class="ui inverted divider" style="height: 50px; width: 300px;"></div>
        </div>
        <%--  <div>
            <div class="ui active inverted dimmer" style="height: 50px; width: 300px;">
                <div class="ui large  text loader">Loading</div>
            </div>
        </div>
        <div>
            <div class="ui active inverted dimmer" style="height: 50px; width: 300px;">
                <div class="ui text loader">正在加载中...</div>
            </div>
        </div>--%>
        <div>
            <div class="ui segment">
                <h2 class="ui left floated header">Left Header</h2>
                <h2 class="ui right floated header">Right Header</h2>
                <div class="ui clearing divider"></div>
                <p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Donec odio. Quisque volutpat mattis eros. Nullam malesuada erat ut turpis. Suspendisse urna nibh, viverra non, semper suscipit, posuere a, pede.</p>
            </div>
        </div>

        <div>
            <div class="ui two column middle aligned relaxed grid basic segment">
                <div class="column">
                    <div class="ui form segment">
                        <div class="field">
                            <label>Username</label>
                            <div class="ui left labeled icon input">
                                <input type="text" placeholder="Username">
                                <i class="user icon"></i>
                                <div class="ui corner label">
                                    <i class="asterisk icon"></i>
                                </div>
                            </div>
                        </div>
                        <div class="field">
                            <label>Password</label>
                            <div class="ui left labeled icon input">
                                <input type="password">
                                <i class="lock icon"></i>
                                <div class="ui corner label">
                                    <i class="asterisk icon"></i>
                                </div>
                            </div>
                        </div>
                        <div class="ui blue submit button">Login</div>
                    </div>
                </div>
                <div class="ui vertical divider">
                    Or
                </div>
                <div class="center aligned column">
                    <div class="huge green ui labeled icon button">
                        <i class="signup icon"></i>
                        Sign Up
                    </div>
                </div>
            </div>
        </div>



        <div style="width: 600px;">
            <i class="archive icon green"></i>archive
            <i class="attachment icon"></i>
            <i class="browser icon"></i>
            <i class="bug icon"></i>
            <i class="calendar icon"></i>
            <i class="cart icon"></i>
            <i class="certificate icon"></i>
            <i class="chat icon"></i>
            <i class="cloud icon"></i>
            <i class="code icon"></i>
            <i class="comment icon"></i>
            <i class="dashboard icon"></i>
            <i class="desktop icon"></i>
            <i class="empty calendar icon"></i>
            <i class="external url icon"></i>
            <i class="external url sign icon"></i>
            <i class="file icon"></i>
            <i class="file outline icon"></i>
            <i class="folder icon"></i>
            <i class="open folder icon"></i>
            <i class="open folder outline icon"></i>
            <i class="folder outline icon"></i>
            <i class="help icon"></i>
            <i class="home icon"></i>
            <i class="inbox icon"></i>
            <i class="info icon"></i>
            <i class="info letter icon"></i>
            <i class="legal icon"></i>
            <i class="location arrow icon"></i>
            <i class="mail icon"></i>
            <i class="mail outline icon"></i>
            <i class="map icon"></i>
            <i class="map marker icon"></i>
            <i class="mobile icon"></i>
            <i class="music icon"></i>
            <i class="chat outline icon"></i>
            <i class="comment outline icon"></i>
            <i class="payment icon"></i>
            <i class="photo icon"></i>
            <i class="qr code icon"></i>
            <i class="question icon"></i>
            <i class="rss icon"></i>
            <i class="rss sign icon"></i>
            <i class="setting icon"></i>
            <i class="settings icon"></i>
            <i class="signal icon"></i>
            <i class="sitemap icon"></i>
            <i class="table icon"></i>
            <i class="tablet icon"></i>
            <i class="tag icon"></i>
            <i class="tags icon"></i>
            <i class="tasks icon"></i>
            <i class="terminal icon"></i>
            <i class="text file icon"></i>
            <i class="text file outline icon"></i>
            <i class="time icon"></i>
            <i class="trash icon"></i>
            <i class="url icon"></i>
            <i class="user icon"></i>
            <i class="users icon"></i>
            <i class="video icon"></i>


        </div>



        <div>

            <div class="ui five connected items">
                <div class="item ">
                    <div class="image">
                        <img src="http://semantic-ui.com/images/demo/highres4.jpg">
                        <a class="star ui corner label green">
                            <i class="star icon arrow "></i>
                        </a>
                    </div>
                    <div class="content action">
                        <div class="name">Cute Dog</div>
                        <p class="description">This dog has some things going for it. Its pretty cute and looks like it'd be fun to cuddle up with.</p>
                    </div>
                </div>
                <div class="item">
                    <div class="image">
                        <img src="http://semantic-ui.com/images/demo/highres5.jpg">
                        <a class="star ui corner label">
                            <i class="star icon"></i>
                        </a>
                    </div>
                    <div class="content">
                        <div class="name">Faithful Dog</div>
                        <p class="description">Sometimes its more important to have a dog you know you can trust. But not every dog is trustworthy, you can tell by looking at its smile.</p>
                    </div>
                </div>
                <div class="item">
                    <div class="image">
                        <img src="http://semantic-ui.com/images/demo/highres3.jpg">
                        <a class="star ui corner label">
                            <i class="star icon"></i>
                        </a>
                    </div>
                    <div class="content">
                        <div class="name">Silly Dog</div>
                        <p class="description">Silly dogs can be quite fun to have as companions. You never know what kind of ridiculous thing they will do.</p>
                    </div>
                </div>
                <div class="item">
                    <div class="image">
                        <img src="http://semantic-ui.com/images/demo/highres2.jpg">
                        <a class="star ui corner label">
                            <i class="star icon"></i>
                        </a>
                    </div>
                    <div class="content">
                        <div class="name">Happy Dog</div>
                        <p class="description">Happy dogs are pretty interesting if you are an unhappy person.</p>
                    </div>
                </div>
                <div class="item">
                    <div class="image">
                        <img src="http://semantic-ui.com/images/demo/highres.jpg">
                        <a class="star ui corner label">
                            <i class="star icon"></i>
                        </a>
                    </div>
                    <div class="content">
                        <div class="name">Quiet Dog</div>
                        <p class="description">A quiet dog is nice if you dont like a lot of upkeep for your dogs.</p>
                    </div>
                </div>
            </div>

        </div>


        <div>


            <div class="ui accordion">
                <div class="active title">
                    <i class="dropdown icon"></i>
                    What is a dog?
                </div>
                <div class=" content">
                    <p>A dog is a type of domesticated animal. Known for its loyalty and faithfulness, it can be found as a welcome guest in many households across the world.</p>
                </div>
                <div class="title">
                    <i class="dropdown icon"></i>
                    What kinds of dogs are there?
                </div>
                <div class="content">
                    <p>There are many breeds of dogs. Each breed varies in size and temperament. Owners often select a breed of dog that they find to be compatible with their own lifestyle and desires from a companion.</p>
                </div>
                <div class="title">
                    <i class="dropdown icon"></i>
                    How do you acquire a dog?
                </div>
                <div class="content">
                    <p>Three common ways for a prospective owner to acquire a dog is from pet shops, private owners, or shelters.</p>
                    <p>A pet shop may be the most convenient way to buy a dog. Buying a dog from a private owner allows you to assess the pedigree and upbringing of your dog before choosing to take it home. Lastly, finding your dog from a shelter, helps give a good home to a dog who may not find one so readily.</p>
                </div>
            </div>

        </div>
    </form>
</body>
</html>

